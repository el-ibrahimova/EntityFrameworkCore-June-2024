using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Artillery.Data.Models;
using Artillery.DataProcessor.ImportDto;

namespace Artillery.DataProcessor
{
    using Artillery.Data;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Countries");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCountryDto[]), root);

            using StringReader reader = new StringReader(xmlString);
            var countriesDtos = (ImportCountryDto[])serializer.Deserialize(reader);

            List<Country> validCountries = new();
            StringBuilder sb = new();

            foreach (var countryDto in countriesDtos)
            {
                if (!IsValid(countryDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Country country = new Country()
                {
                    CountryName = countryDto.CountryName,
                    ArmySize = countryDto.ArmySize
                };

                validCountries.Add(country);
                sb.AppendLine(string.Format(SuccessfulImportCountry, country.CountryName, country.ArmySize));
            }

            context.Countries.AddRange(validCountries);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Manufacturers");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportManufacturerDto[]), root);

            using StringReader reader = new StringReader(xmlString);
            var manufacturersDtos = (ImportManufacturerDto[])serializer.Deserialize(reader);

            List<Manufacturer> validManufacturers = new();
            StringBuilder sb = new();

            foreach (var manDto in manufacturersDtos)
            {
                if (!IsValid(manDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (validManufacturers.Any(m => m.ManufacturerName == manDto.ManufacturerName))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Manufacturer manufacturer = new Manufacturer()
                {
                    ManufacturerName = manDto.ManufacturerName,
                    Founded = manDto.Founded
                };

                validManufacturers.Add(manufacturer);
                string[] foundedInfo = manufacturer.Founded.Split(", ");

                string foundInfo = "";
                for (int i = 0; i < foundedInfo.Length; i++)
                {
                    string town = foundedInfo[foundedInfo.Length-2];
                    string country = foundedInfo[foundedInfo.Length-1];
                    foundInfo = $"{town}, {country}";
                }
                
                sb.AppendLine(string.Format(SuccessfulImportManufacturer, manufacturer.ManufacturerName, foundInfo));
            }

            context.Manufacturers.AddRange(validManufacturers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            throw new NotImplementedException();
        }
        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}