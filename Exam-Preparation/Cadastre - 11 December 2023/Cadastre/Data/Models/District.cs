using System.ComponentModel.DataAnnotations;
using Cadastre.Common;
using Cadastre.Data.Enumerations;
using Castle.DynamicProxy.Generators.Emitters;

namespace Cadastre.Data.Models
{
    public class District
    {
        public District()
        {
            this.Properties = new HashSet<Property>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.DistrictNameMaxLength)] 
        public string Name { get; set; } = null!;

        [MaxLength(ValidationConstants.DistrictPostalCodeMaxLength)] 
        public string PostalCode { get; set; } = null!;

        [Required]
        public Region Region {get; set;}

        public virtual ICollection<Property> Properties { get; set; }
        
    }
}
