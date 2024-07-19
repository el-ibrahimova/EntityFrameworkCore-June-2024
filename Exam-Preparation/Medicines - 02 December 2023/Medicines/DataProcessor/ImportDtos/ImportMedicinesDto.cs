using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Medicines.Common.ValidationConstants;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Medicine")]
    public class ImportMedicinesDto
    {
        [XmlAttribute("category")]
        [Range(MedicineCategoryMinValue, MedicineCategoryMaxValue)]
        [Required]
        public int Category { get; set; }

        [MinLength(MedicineNameMinLength)]
        [MaxLength(MedicineNameMaxLength)]
        [Required]
        public string Name { get; set; } = null!;

        [Range(MedicinePriceMinValue, MedicinePriceMaxValue)]
        [Required]
        public decimal Price { get; set; }

        [Required] 
        public string ProductionDate { get; set; } = null!;

        [Required] 
        public string ExpiryDate { get; set; } = null!;

        [MinLength(MedicineProducerNameMinLength)]
        [MaxLength(MedicineProducerNameMaxLength)]
        [Required]
        public string Producer { get; set; } = null!;
    }
}
