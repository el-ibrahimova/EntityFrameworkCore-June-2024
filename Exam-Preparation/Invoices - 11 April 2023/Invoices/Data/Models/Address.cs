using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Invoices.Common;
using static Invoices.Common.ValidationConstants;

namespace Invoices.Data.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AddressStreetNameMaxLength)]
        public string StreetName { get; set; } = null!;

        [Required]
        public int StreetNumber { get; set; }

        [Required] 
        [MaxLength(AddressPostCodeMaxLength)] 
        public string PostCode { get; set; } = null!;

        [Required]
        [MaxLength(AddressCityMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(AddressCountryMaxLength)]
        public string Country { get; set; } = null!;

     
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;

    }
}
