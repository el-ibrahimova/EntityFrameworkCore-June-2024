using System.ComponentModel.DataAnnotations;
using Cadastre.Common;
using Cadastre.Data.Enumerations;

namespace Cadastre.Data.Models
{
    public class Citizen
    { public Citizen()
        {
            this.PropertiesCitizens = new HashSet<PropertyCitizen>();
        }

        [Key]
        public int Id { get; set; }

        [ MaxLength(ValidationConstants.CitizenNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [MaxLength(ValidationConstants.CitizenNameMaxLength)]
        public string LastName { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        [Required]
        public MaritalStatus MaritalStatus { get; set; }

        public virtual ICollection<PropertyCitizen> PropertiesCitizens { get; set; }
    }
}
