using System.Diagnostics;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
using TravelAgency.Data;
using TravelAgency.Data.Models.Enums;
using TravelAgency.DataProcessor.ExportDtos;
using Formatting = Newtonsoft.Json.Formatting;

namespace TravelAgency.DataProcessor
{
    public class Serializer
    {
        public static string ExportGuidesWithSpanishLanguageWithAllTheirTourPackages(TravelAgencyContext context)
        {
            var spanishSpeakingGuides = context.Guides
                .Where(g => g.Language == Language.Spanish)
                .Select(g => new ExportSpanishSpeakingGuidesDto()
                {
                    FullName = g.FullName,
                    TourPackages = g.TourPackagesGuides
                        .Select(t => new ExportTourPackagesDto()
                        {
                            Name = t.TourPackage.PackageName,
                            Description = t.TourPackage.Description,
                            Price = t.TourPackage.Price
                        })
                        .OrderByDescending(t=>t.Price)
                        .ThenBy(t=>t.Name)
                        .ToArray()
                })
                .OrderByDescending(g=>g.TourPackages.Length)
                .ThenBy(g=>g.FullName)
                .ToArray();
            
          return Serialize(spanishSpeakingGuides, "Guides");
        }

        public static string ExportCustomersThatHaveBookedHorseRidingTourPackage(TravelAgencyContext context)
        {
            var horseRidingCustomers = context.Customers
                .Where(c => c.Bookings.Any(b => b.TourPackage.PackageName == "Horse Riding Tour"))
                .Select(c => new ExportHorseRidingCustomersDto()
                {
                    FullName = c.FullName,
                    PhoneNumber = c.PhoneNumber,
                    Bookings = c.Bookings
                        .Where(b=>b.TourPackage.PackageName == "Horse Riding Tour")
                         .OrderBy(b=>b.BookingDate) 
                         .Select(b => new ExportHorseRidingToursDto()
                        {
                            TourPackageName = b.TourPackage.PackageName,
                            Date = b.BookingDate.ToString("yyyy-MM-dd")
                        })
                        .ToArray()
                })
                .OrderByDescending(c=>c.Bookings.Length)
                .ThenBy(c=>c.FullName)
                .ToArray();

          return JsonConvert.SerializeObject(horseRidingCustomers, Formatting.Indented);
        }

        public static string Serialize<T>(T obj, string rootName, bool omitXmlDeclaration = false)
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
