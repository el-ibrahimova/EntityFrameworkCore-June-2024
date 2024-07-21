using Trucks.DataProcessor.ExportDto;

namespace Trucks.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using System.Diagnostics;
    using System.Text;
    using System.Xml.Serialization;
    using System.Xml;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            var despatchersWithTrucks = context.Despatchers
                .Where(d => d.Trucks.Any())
                .Select(d => new ExportDespatchersWithTrucksDto()
                {
                    TrucksCount = d.Trucks.Count,
                    Name = d.Name,
                    Trucks = d.Trucks
                        .Select(t => new ExportTrucksDespatcherDto()
                        {
                            RegistrationNumber = t.RegistrationNumber,
                            Make = t.MakeType.ToString()
                        })
                        .OrderBy(t=>t.RegistrationNumber)
                        .ToArray()
                })
                .OrderByDescending(d=>d.TrucksCount)
                .ThenBy(d=>d.Name)
                .ToArray();

            return SerializeToXml(despatchersWithTrucks, "Despatchers");
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            var clientsWithTrucks = context.Clients
                .Where(c => c.ClientsTrucks.Any(t => t.Truck.TankCapacity >= capacity))
                
                .Select(c => new ExportClientsWithTrucksDto()
                {
                    Name = c.Name,
                    Trucks = c.ClientsTrucks
                        .Where(t => t.Truck.TankCapacity >= capacity)
                         .OrderBy(t => t.Truck.MakeType)
                        .ThenByDescending(t => t.Truck.CargoCapacity).Select(t => new ExportTrucksDto()
                        {
                            RegistrationNumber = t.Truck.RegistrationNumber,
                            VinNumber = t.Truck.VinNumber,
                            TankCapacity = t.Truck.TankCapacity,
                            CargoCapacity = t.Truck.CargoCapacity,
                            CategoryType = t.Truck.CategoryType.ToString(),
                            MakeType = t.Truck.MakeType.ToString()
                        })
                        .ToArray()
                })
                .OrderByDescending(c => c.Trucks.Count())
                .ThenBy(c => c.Name)
               .Take(10) 
                .ToArray();

            return JsonConvert.SerializeObject(clientsWithTrucks, Newtonsoft.Json.Formatting.Indented);
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
