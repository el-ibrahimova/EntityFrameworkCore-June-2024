using System.Text;
using System.Xml.Serialization;
using Medicines.Data.Models;
using Medicines.Data.Models.Enums;
using Medicines.DataProcessor.ImportDtos;
using Newtonsoft.Json;

namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            var patientsDtos = JsonConvert.DeserializeObject<ImportPatientsDto[]>(jsonString);

            List<Patient> validPatients = new();

            StringBuilder sb = new();

            foreach (var patientDto in patientsDtos)
            {
                if (!IsValid(patientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Patient patient = new Patient()
                {
                    FullName = patientDto.FullName,
                    AgeGroup = (AgeGroup)patientDto.AgeGroup,
                    Gender = (Gender)patientDto.Gender
                };

                foreach (var medicineId in patientDto.Medicines)
                {

                    if (patient.PatientsMedicines.Any(s => s.MedicineId == medicineId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    PatientMedicine patientMedicine = new PatientMedicine()
                    {
                        Patient = patient,
                        MedicineId = medicineId
                    };
                    patient.PatientsMedicines.Add(patientMedicine);
                }
                validPatients.Add(patient);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient, patient.FullName, patient.PatientsMedicines.Count));
            }
            context.Patients.AddRange(validPatients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Pharmacies");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportPharmaciesDto[]), root);

            using StringReader reader = new StringReader(xmlString);

            var pharmaciesDtos = (ImportPharmaciesDto[])serializer.Deserialize(reader);

            List<Pharmacy> pharmacyList = new();
            StringBuilder sb = new();

            foreach (var pharmacyDto in pharmaciesDtos)
            {
                if (!IsValid(pharmacyDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Pharmacy pharmacy = new Pharmacy()
                {
                    IsNonStop = bool.Parse(pharmacyDto.IsNonStop),
                    Name = pharmacyDto.Name,
                    PhoneNumber = pharmacyDto.PhoneNumber
                };

                foreach (var medicineDto in pharmacyDto.Medicines)
                {
                    if (!IsValid(medicineDto))
                    {
                        sb.AppendLine(ErrorMessage); 
                        continue;
                    }

                    // validate production date
                    DateTime medicineProductionDate;
                    bool isProductionDateValid = DateTime.TryParseExact(medicineDto.ProductionDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out medicineProductionDate);

                    if (!isProductionDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    // validate date of expiry
                    DateTime medicineExpiryDate;
                    bool isExpiryDateValid = DateTime.TryParseExact(medicineDto.ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out medicineExpiryDate);

                    if (!isExpiryDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    // check for valid date
                    if (medicineProductionDate >= medicineExpiryDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    // check if medicine name and producer name already exist 
                    if (pharmacy.Medicines.Any(x => x.Name == medicineDto.Name && x.Producer == medicineDto.Producer))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Medicine medicine = new Medicine()
                    {
                        Category = (Category)medicineDto.Category, // enum
                        Name = medicineDto.Name,
                        Price = medicineDto.Price,
                        ProductionDate = medicineProductionDate,
                        ExpiryDate = medicineExpiryDate,
                        Producer = medicineDto.Producer
                    };

                    pharmacy.Medicines.Add(medicine);
                }
                pharmacyList.Add(pharmacy);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, pharmacy.Name, pharmacy.Medicines.Count));
            }

            context.Pharmacies.AddRange(pharmacyList);
            context.SaveChanges();

            return sb.ToString().ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
