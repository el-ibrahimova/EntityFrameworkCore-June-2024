using System.Globalization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Theatre.Data.Models.Enums;
using Theatre.DataProcessor.ExportDto;
using Theatre.DataProcessor.ImportDto;

namespace Theatre.DataProcessor
{

    using System;
    using System.Diagnostics;
    using System.Text;
    using System.Xml;
    using Theatre.Data;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var exportTheatres = context.Theatres
                .Where(t => t.NumberOfHalls >= numbersOfHalls && t.Tickets.Count >= 20)
                .Select(t => new ExportTheatresDto()
                {
                    Name = t.Name,
                    Halls = t.NumberOfHalls,
                    TotalIncome = t.Tickets.Where(ti => ti.RowNumber >= 1 && ti.RowNumber <= 5).Sum(ti => ti.Price),
                    Tickets = t.Tickets
                     .Where(x => x.RowNumber >= 1 && x.RowNumber <= 5)
                     .Select(ti => new ExportTicketsDto()
                     {
                         Price = ti.Price,
                         RowNumber = ti.RowNumber
                     })
                     .OrderByDescending(ti => ti.Price)
                     .ToArray()
                })
                .OrderByDescending(t => t.Halls)
                .ThenBy(t => t.Name)
                .ToArray();

            return JsonConvert.SerializeObject(exportTheatres, Newtonsoft.Json.Formatting.Indented);
        }

        public static string ExportPlays(TheatreContext context, double raiting)
        {
            var plays = context.Plays
                .Where(p => p.Rating <= raiting)
                .Select(p => new ExportPlaysDto()
                {
                    Title = p.Title,
                    Duration = p.Duration.ToString("c", CultureInfo.InvariantCulture),
                    Rating = p.Rating == 0 ? "Premier" : p.Rating.ToString(),
                    Genre = p.Genre.ToString(),
                    Actors = p.Casts.Where(p => p.IsMainCharacter)
                        .Select(a => new ExportActorsDto()
                        {
                            FullName = a.FullName,
                            MainCharacter = $"Plays main character in '{a.Play.Title}'."
                        })
                        .OrderByDescending(a=>a.FullName)
                        .ToArray()
                        })
                .OrderBy(p=>p.Title)
                .ThenByDescending(p=>p.Genre)
                .ToArray();

            return SerializeToXml(plays, "Plays");
        }

        public static string SerializeToXml<T>(T obj, string rootName, bool omitXmlDeclaration = false)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Object to serialize cannot be null.");

            if (string.IsNullOrEmpty(rootName))
                throw new ArgumentNullException(nameof(rootName), "Root name cannot be null or empty.");

            try
            {
                XmlRootAttribute xmlRoot = new(rootName);
                XmlSerializer xmlSerializer = new(typeof(T), xmlRoot);

                XmlSerializerNamespaces namespaces = new();
                namespaces.Add(string.Empty, string.Empty);

                XmlWriterSettings settings = new()
                {
                    OmitXmlDeclaration = omitXmlDeclaration,
                    Indent = true
                };

                StringBuilder sb = new();
                using var stringWriter = new StringWriter(sb);
                using var xmlWriter = XmlWriter.Create(stringWriter, settings);

                xmlSerializer.Serialize(xmlWriter, obj, namespaces);
                return sb.ToString().TrimEnd();
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine($"Serialization error: {ex.Message}");
                throw new InvalidOperationException($"Serializing {typeof(T)} failed.", ex);
            }
        }

    }
}
