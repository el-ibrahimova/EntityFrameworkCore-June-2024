using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Data;
using Artillery.Data.Models;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType(nameof(Shell))]
    public class ImportShellDto
    {
        [Required]
        [XmlElement(nameof(ShellWeight))]
        [Range(typeof(double),ValidationConstants.ShellWeightMinValue, ValidationConstants.ShellWeightMaxValue)]
        public double ShellWeight { get; set; }

        [Required]
        [MinLength(ValidationConstants.ShellCaliberMinLength)]
        [MaxLength(ValidationConstants.ShellCaliberMaxLength)]
        [XmlElement(nameof(Caliber))]
        public string Caliber { get; set; } = null!;
    }
}
