using Medicines.Data.Models.Enums;
using Medicines.DataProcessor.ExportDtos;
using Formatting = Newtonsoft.Json.Formatting;

namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using System.Diagnostics;
    using System.Text;
    using System.Xml.Serialization;
    using System.Xml;
    using Newtonsoft.Json;
    using System.Globalization;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            var inputDataFormat = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var patientsWithMedicines = context.Patients
                .Where(p => p.PatientsMedicines.Any(p => p.Medicine.ProductionDate > inputDataFormat))
                .Select(p => new ExportPatientsWithMedicinesDto()
                {
                    Gender = p.Gender.ToString().ToLowerInvariant(),
                    Name = p.FullName,
                    AgeGroup = p.AgeGroup.ToString(),
                    Medicines = p.PatientsMedicines
                        .Where(pm => pm.Medicine.ProductionDate > inputDataFormat)
                         .OrderByDescending(pm => pm.Medicine.ExpiryDate)
                        .ThenBy(pm => pm.Medicine.Price)
                        .Select(pm => new ExportMedicinesBestBeforeDto()
                        {
                            Category = pm.Medicine.Category.ToString().ToLowerInvariant(),
                            Name = pm.Medicine.Name,
                            Price = pm.Medicine.Price.ToString("f2"),
                            Producer = pm.Medicine.Producer,
                            ExpiryDate = pm.Medicine.ExpiryDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        })

                        .ToList()
                })
                .OrderByDescending(p => p.Medicines.Count)
                .ThenBy(p => p.Name)
                .ToList();

            return SerializeToXml(patientsWithMedicines, "Patients");
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
            var medicinesByCategory = context.Medicines
                .Where(m => (int)m.Category == medicineCategory && m.Pharmacy.IsNonStop == true)
                .OrderBy(s => s.Price)
                .ThenBy(s => s.Name)
             .Select(m => new ExportMedicinesDto()
             {
                 Name = m.Name,
                 Price = m.Price.ToString("f2"),
                 Pharmacy = new ExportPharmaciesDto()
                 {
                     Name = m.Pharmacy.Name,
                     PhoneNumber = m.Pharmacy.PhoneNumber
                 }
             })
                .ToList();


            return JsonConvert.SerializeObject(medicinesByCategory, Newtonsoft.Json.Formatting.Indented);
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
