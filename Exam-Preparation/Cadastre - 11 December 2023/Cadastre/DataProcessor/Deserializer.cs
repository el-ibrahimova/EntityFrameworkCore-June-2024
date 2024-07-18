using System.Collections;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using Cadastre.Data.Enumerations;
using Cadastre.Data.Models;
using Cadastre.DataProcessor.ImportDtos;
using Newtonsoft.Json;

namespace Cadastre.DataProcessor
{
    using Cadastre.Data;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid Data!";
        private const string SuccessfullyImportedDistrict =
            "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen =
            "Succefully imported citizen - {0} {1} with {2} properties.";

        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {
            XmlRootAttribute root = new XmlRootAttribute("Districts");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportDistrictsDto[]), root);

            using StringReader reader = new StringReader(xmlDocument);

            var districtsDtos = (ImportDistrictsDto[])serializer.Deserialize(reader);

            List<District> districtList = new();

            StringBuilder sb = new();

            foreach (var districtDto in districtsDtos)
            {
                if (!IsValid(districtDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                // check if duplicate District name
                if (dbContext.Districts.Any(d => d.Name == districtDto.Name))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var district = new District()
                {
                    Region = (Region)Enum.Parse(typeof(Region), districtDto.Region),
                    Name = districtDto.Name,
                    PostalCode = districtDto.PostalCode
                };

                foreach (var prop in districtDto.Properties)
                {
                    if (!IsValid(prop))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    // check if PropertyIdentifier is already added or it is duplicated
                    if (dbContext.Properties.Any(p
                            => p.PropertyIdentifier == prop.PropertyIdentifier)
                        || district.Properties.Any(d => d.PropertyIdentifier == prop.PropertyIdentifier))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    // check if Address is already added or it is duplicated
                    if (dbContext.Properties.Any(s
                            => s.Address == prop.Address)
                               || district.Properties.Any(s => s.Address == prop.Address))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var propertyNew = new Data.Models.Property()
                    {
                        PropertyIdentifier = prop.PropertyIdentifier,
                        Area = prop.Area,
                        Details = prop.Details,
                        Address = prop.Address,
                        DateOfAcquisition = DateTime
                            .ParseExact(prop.DateOfAcquisition, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    };

                    district.Properties.Add(propertyNew);
                }

                districtList.Add(district);
                sb.AppendLine(string.Format(SuccessfullyImportedDistrict, district.Name, district.Properties.Count));

            }
            dbContext.Districts.AddRange(districtList);
            dbContext.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {
            StringBuilder sb = new StringBuilder();

            ImportCitizensDto[] citizenDtos = JsonConvert.DeserializeObject<ImportCitizensDto[]>(jsonDocument);

            List<Citizen> validCitizens = new List<Citizen>();


            foreach (var citizenDto in citizenDtos)
            {
                if (!IsValid(citizenDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Citizen newCitizen = new Citizen()
                {
                    FirstName = citizenDto.FirstName,
                    LastName = citizenDto.LastName,
                    BirthDate = DateTime.ParseExact(citizenDto.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    MaritalStatus = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), citizenDto.MaritalStatus)
                };

                foreach (var propId in citizenDto.Properties)
                {
                    PropertyCitizen pc = new PropertyCitizen()
                    {
                        Citizen = newCitizen,
                        PropertyId = propId
                    };

                    newCitizen.PropertiesCitizens.Add(pc);
                }

                validCitizens.Add(newCitizen);
                sb.AppendLine(string.Format(SuccessfullyImportedCitizen, newCitizen.FirstName, newCitizen.LastName, newCitizen.PropertiesCitizens.Count));
            }

            dbContext.Citizens.AddRange(validCitizens);
            dbContext.SaveChanges();

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
