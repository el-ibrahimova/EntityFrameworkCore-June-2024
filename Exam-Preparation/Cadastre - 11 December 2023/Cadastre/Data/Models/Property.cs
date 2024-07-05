using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cadastre.Common;

namespace Cadastre.Data.Models
{
    public class Property
    {
        public Property()
        {
            this.PropertiesCitizens = new HashSet<PropertyCitizen>();
        }

        [Key] public int Id { get; set; }

        [MaxLength(ValidationConstants.PropertyIdentifierMaxLength)] 
        public string PropertyIdentifier { get; set; } = null!;

        [Range(0, int.MaxValue)] 
        public int Area { get; set; }

        [MaxLength(ValidationConstants.PropertyDetailsMaxLength)] 
        public string? Details { get; set; }

        [MaxLength(ValidationConstants.PropertyAddressMaxLength)] 
        public string Address { get; set; } = null!;

        public DateTime DateOfAcquisition { get; set; }

        [ForeignKey(nameof(District))] public int DistrictId { get; set; }
        public virtual District District { get; set; }

        public virtual ICollection<PropertyCitizen> PropertiesCitizens { get; set; }
    }
}
