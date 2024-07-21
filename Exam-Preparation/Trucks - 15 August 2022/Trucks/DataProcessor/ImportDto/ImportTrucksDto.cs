using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Trucks.Common.ValidationConstants;

namespace Trucks.DataProcessor.ImportDto
{
    [XmlType("Truck")]
    public class ImportTrucksDto
    {
        [RegularExpression(TruckRegistrationNumberRegEx)]
        public string RegistrationNumber { get; set; }

        [Required]
        [MinLength(TruckVinNumberMinLength)]
        [MaxLength(TruckVinNumberMaxLength)]
        public string VinNumber { get; set; } = null!;

        [Range(TruckTankCapacityMinValue, TruckTankCapacityMaxValue)]
        public int TankCapacity { get; set; }

        [Range(TruckCargoCapacityMinValue, TruckCargoCapacityMaxValue)]
        public int CargoCapacity { get; set; }

        [Required]
        [Range(TruckCategoryTypeMinValue, TruckCategoryTypeMaxValue)]
        public int CategoryType { get; set; }

        [Required]
        [Range(TruckMakeTypeMinValue, TruckMakeTypeMaxValue)]
        public int MakeType { get; set; }
    }
}
