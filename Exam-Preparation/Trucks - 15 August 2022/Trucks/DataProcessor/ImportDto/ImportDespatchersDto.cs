using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Trucks.Common.ValidationConstants;

namespace Trucks.DataProcessor.ImportDto
{
    [XmlType("Despatcher")]
    public class ImportDespatchersDto
    {
        [Required]
        [MinLength(DespatcherNameMinLength)]
        [MaxLength(DespatcherNameMaxLength)]
        public string Name { get; set; } = null!;

        [MinLength(DespatcherPositionMinLength)]
        [MaxLength(DespatcherPositionMaxLength)]
        public string Position { get; set; }

        [XmlArray("Trucks")] 
        public ImportTrucksDto[] Trucks { get; set; } = null!;

    }
}
