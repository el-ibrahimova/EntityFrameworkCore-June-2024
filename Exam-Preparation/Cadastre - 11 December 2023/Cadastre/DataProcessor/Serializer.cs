using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using Cadastre.Data;
using Cadastre.DataProcessor.ExportDtos;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Cadastre.DataProcessor
{
    public class Serializer
    {
        public static string ExportPropertiesWithOwners(CadastreContext dbContext)
        {
            string date = "01/01/2000";

            var propertiesWithOwners = dbContext.Properties
                .Where(s => s.DateOfAcquisition >=
                            DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                .OrderByDescending(s => s.DateOfAcquisition)
                .ThenBy(p => p.PropertyIdentifier)
                .Select(p => new ExportPropertiesWithOwnersDto()
                {
                    PropertyIdentifier = p.PropertyIdentifier,
                    Area = p.Area,
                    Address = p.Address,
                    DateOfAcquisition = p.DateOfAcquisition.ToString("dd/MM/yyyy"),
                    Owners = p.PropertiesCitizens
                     .Select(ps => new ExportOwnersDto()
                        {
                            LastName = ps.Citizen.LastName,
                            MaritalStatus = ps.Citizen.MaritalStatus.ToString()
                        })
                        .OrderBy(ps => ps.LastName)
                        .ToArray()
                })
                .ToList();

            return JsonConvert.SerializeObject(propertiesWithOwners, Formatting.Indented);
        }

        public static string ExportFilteredPropertiesWithDistrict(CadastreContext dbContext)
        {
            var filteredProperties = dbContext.Properties
                .Where(p => p.Area >= 100)
                .OrderByDescending(p => p.Area)
                .ThenBy(p => p.DateOfAcquisition)
                .ToList()
                .Select(p => new ExportFilteredPropertiesDto()
                {
                    PostalCode = p.District.PostalCode,
                    PropertyIdentifier = p.PropertyIdentifier,
                    Area = p.Area,
                    DateOfAcquisition = p.DateOfAcquisition.ToString("dd/MM/yyyy")
                })
               
                .ToList();

            return SerializeToXml(filteredProperties, "Properties");
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
