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
        [MaxLength(AddressStreetNameMaxLength)]    // NVARCHAR(20)
        public string StreetName { get; set; } = null!; 

        [Required] // int by default is required
        public int StreetNumber { get; set; }

        [Required] 
       public string PostCode { get; set; } = null!;  // NVARCHAR(MAX)

        [Required]
        [MaxLength(AddressCityMaxLength)]    // NVARCHAR(15)
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(AddressCountryMaxLength)]    // NVARCHAR(15)
        public string Country { get; set; } = null!;  

     
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;

    }
}
