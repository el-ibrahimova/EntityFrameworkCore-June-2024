using Artillery.Data;
using System.ComponentModel.DataAnnotations;

namespace Artillery.DataProcessor.ImportDto
{
    public class ImportGunDto
    {
        public int ManufacturerId { get; set; }

        [Required]
        [Range(ValidationConstants.GunWeightMinValue, ValidationConstants.GunWeightMaxValue)]
        public int GunWeight { get; set; }

        [Required]
        [Range(typeof(double), ValidationConstants.GunBarrelMinValue, ValidationConstants.GunBarrelMaxValue)]
        public double BarrelLength { get; set; }


        public int? NumberBuild { get; set; }

        [Required]
        [Range(ValidationConstants.GunRangeMinValue, ValidationConstants.GunRangeMaxValue)]
        public int Range { get; set; }

        // it throws error when executing program with this code
        // [Required]
        // [Range(ValidationConstants.GunTypeMinValue, ValidationConstants.GunTypeMaxValue)]
        // public string GunType { get; set; } = null!;

        [Required]
        public string GunType { get; set; } = null!;

        [Required]
        public int ShellId { get; set; }

        public ImportGunCountriesDto[] Countries { get; set; }
    }
}
