using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Artillery.Data.Models;
using Artillery.Data.Models.Enums;
using Artillery.DataProcessor.ImportDto;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
            XmlRootAttribute root = new XmlRootAttribute("Shells");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportShellDto[]), root);

            using StringReader reader = new StringReader(xmlString);

            var shellsDtos = (ImportShellDto[])serializer.Deserialize(reader);

            List<Shell> validShells = new();
            StringBuilder sb = new();

            foreach (var shellDto in shellsDtos)
            {
                if (!IsValid(shellDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Shell shell = new Shell()
                {
                    ShellWeight = shellDto.ShellWeight,
                    Caliber = shellDto.Caliber
                };

                validShells.Add(shell);
                sb.AppendLine(string.Format(SuccessfulImportShell, shell.Caliber, shell.ShellWeight));
            }

            context.Shells.AddRange(validShells);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            var gunsDtos = JsonConvert.DeserializeObject<ImportGunDto[]>(jsonString);

            List<Gun> validGuns = new();
            StringBuilder sb = new();

            // create string [] with possible enum values. It shows error if i add constraint in dto for possible int values 0-5 
            string[] validGunTypes = { "Howitzer", "Mortar", "FieldGun", "AntiAircraftGun", "MountainGun", "AntiTankGun" };

            foreach (var gunDto in gunsDtos)
            {
                if (!IsValid(gunDto) || !validGunTypes.Contains(gunDto.GunType))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Gun gun = new Gun()
                {
                    ManufacturerId = gunDto.ManufacturerId,
                    GunWeight = gunDto.GunWeight,
                    BarrelLength = gunDto.BarrelLength,
                    NumberBuild = gunDto.NumberBuild,
                    Range = gunDto.Range,
                    GunType = (GunType)Enum.Parse(typeof(GunType), gunDto.GunType),
                    ShellId = gunDto.ShellId
                };

                foreach (var countryId in gunDto.Countries)
                {
                    CountryGun countryGun = new CountryGun()
                    {
                        CountryId = countryId.Id,
                        Gun = gun,
                    };
                    gun.CountriesGuns.Add(countryGun);
                }
                sb.AppendLine(string.Format(SuccessfulImportGun, gun.GunType, gun.GunWeight, gun.BarrelLength));
                validGuns.Add(gun);
            }
            
            context.Guns.AddRange(validGuns);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
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