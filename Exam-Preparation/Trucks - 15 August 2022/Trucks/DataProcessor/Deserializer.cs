using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Trucks.Data.Models;
using Trucks.Data.Models.Enums;
using Trucks.DataProcessor.ImportDto;

namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using Data;


    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Despatchers");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportDespatchersDto[]), root);

            using StringReader reader = new StringReader(xmlString);

            var despatchersDtos = (ImportDespatchersDto[])serializer.Deserialize(reader);

            List<Despatcher> validDespatchers = new();
            StringBuilder sb = new();

            foreach (var despatcherDto in despatchersDtos)
            {
                if (!IsValid(despatcherDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (string.IsNullOrEmpty(despatcherDto.Position))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Despatcher despatcher = new Despatcher()
                {
                    Name = despatcherDto.Name,
                    Position = despatcherDto.Position,
                };

                foreach (var truckDto in despatcherDto.Trucks)
                {
                    if (!IsValid(truckDto) || string.IsNullOrWhiteSpace(truckDto.RegistrationNumber))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Truck truck = new Truck()
                    {
                        RegistrationNumber = truckDto.RegistrationNumber,
                        VinNumber = truckDto.VinNumber,
                        TankCapacity = truckDto.TankCapacity,
                        CargoCapacity = truckDto.CargoCapacity,
                        CategoryType = (CategoryType)truckDto.CategoryType,
                        MakeType = (MakeType)truckDto.MakeType
                    };

                    despatcher.Trucks.Add(truck);
                }
                validDespatchers.Add(despatcher);
                sb.AppendLine(string.Format(SuccessfullyImportedDespatcher, despatcher.Name,
                        despatcher.Trucks.Count()));
            }
            context.Despatchers.AddRange(validDespatchers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
        public static string ImportClient(TrucksContext context, string jsonString)
        {
            var clientsDtos = JsonConvert.DeserializeObject<ImportClientsDto[]>(jsonString);

            List<Client> validClients = new();
            StringBuilder sb = new();

            var existingTrucks = context.Trucks
                .Select(t => t.Id)
                .ToList();

            foreach (var clientDto in clientsDtos)
            {
                if (!IsValid(clientDto) || clientDto.Type == "usual")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client client = new Client()
                {
                    Name = clientDto.Name,
                    Nationality = clientDto.Nationality,
                    Type = clientDto.Type,
                };

                foreach (var truckId in clientDto.Trucks.Distinct())
                {
                    if (!existingTrucks.Contains(truckId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    ClientTruck clientTruck = new ClientTruck()
                    {
                        Client = client,
                        TruckId = truckId
                    };

                    client.ClientsTrucks.Add(clientTruck);
                }

                validClients.Add(client);
                sb.AppendLine(string.Format(SuccessfullyImportedClient, client.Name, client.ClientsTrucks.Count));
            }

            context.Clients.AddRange(validClients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}