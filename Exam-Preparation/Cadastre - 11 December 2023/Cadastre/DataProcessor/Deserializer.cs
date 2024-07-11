using System.Collections;
using System.Globalization;
using System.Text;
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
            throw new NotImplementedException();
        }


        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {
            StringBuilder sb = new StringBuilder();

            ImportCitizensDto[] citizenDtos = JsonConvert.DeserializeObject<ImportCitizensDto[]>(jsonDocument);

            ICollection<Citizen> validCitizens = new HashSet<Citizen>();
            
            ICollection<int> validProperties = dbContext.Properties
                .Select(p => p.Id).ToArray();

            foreach (var citizenDto in citizenDtos)
            {
                if (!IsValid(citizenDto)
                    || !Enum.TryParse(typeof(MaritalStatus), citizenDto.MaritalStatus, true, out object result))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Citizen newCitizen = new Citizen()
                {
                    FirstName = citizenDto.FirstName,
                    LastName = citizenDto.LastName,
                    BirthDate = DateTime.ParseExact(citizenDto.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    MaritalStatus = (MaritalStatus)result
                };
                
                foreach (var propId in citizenDto.Properties.Distinct())
                {
                    if (!validProperties.Contains(propId))
                    {
                        continue;
                    }

                    PropertyCitizen pc = new PropertyCitizen()
                    {
                        Citizen = newCitizen,
                        PropertyId = propId
                    };

                    newCitizen.PropertiesCitizens.Add(pc);
                }

                sb.AppendLine(string.Format(SuccessfullyImportedCitizen,
                    newCitizen.FirstName,
                    newCitizen.LastName,
                  newCitizen.PropertiesCitizens.Count()));

                validCitizens.Add(newCitizen);
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
