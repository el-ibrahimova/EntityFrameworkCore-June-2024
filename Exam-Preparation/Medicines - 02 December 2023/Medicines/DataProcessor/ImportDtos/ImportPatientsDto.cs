using System.ComponentModel.DataAnnotations;
using static Medicines.Common.ValidationConstants;

namespace Medicines.DataProcessor.ImportDtos
{
    public class ImportPatientsDto
    {
        [Required]
        [MinLength(PatientFullNameMinLength)]
        [MaxLength(PatientFullNameMaxLength)]
        public string FullName { get; set; } = null!;

        [Required]
        [Range(PatientAgeGroupMinValue, PatientAgeGroupMaxValue)]
        public int AgeGroup { get; set; }

        [Required]
        [Range(PatientGenderMinValue, PatientGenderMaxValue)]
        public int Gender { get; set; }

        public int[] Medicines { get; set; } = null!;
    }
}
