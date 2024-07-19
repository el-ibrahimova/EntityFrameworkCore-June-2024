using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Medicines.Common;
using Medicines.Data.Models.Enums;

namespace Medicines.Data.Models
{
    public class Pharmacy
    {
        public Pharmacy()
        {
            this.Medicines = new HashSet<Medicine>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.PharmacyNameMaxLength)]
        [Required]
        public string Name { get; set; } = null!;

        [MaxLength(ValidationConstants.PharmacyPhoneNumberMaxLength)]
        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public bool IsNonStop { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; }

    }
}
