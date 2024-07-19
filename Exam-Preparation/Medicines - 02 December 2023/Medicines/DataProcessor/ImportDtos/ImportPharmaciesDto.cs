using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Medicines.Common.ValidationConstants;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Pharmacy")]
    public class ImportPharmaciesDto
    {
        [XmlAttribute("non-stop")]
        [RegularExpression(@"^(true|false)$")]
        [Required]
        public string IsNonStop { get; set; } = null!;

        [MinLength(PharmacyNameMinLength)]
        [MaxLength(PharmacyNameMaxLength)]
        [Required]
        public string Name { get; set; } = null!;

       [RegularExpression(PharmacyPhoneNumberRegEx)]
        [Required]
        public string PhoneNumber { get; set; } = null!;


        [XmlArray("Medicines")] 
        public ImportMedicinesDto[] Medicines { get; set; } = null!;

    }
}
