using System.ComponentModel.DataAnnotations;
using Invoices.Common;

namespace Invoices.Data.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ClientNameMaxLength)]  // NVARCHAR(25)
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.ClientNumberVatMaxLength)]  // NVARCHAR(15)
        public string NumberVat { get; set; } = null!;

        public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
        public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();

        public virtual ICollection<ProductClient> ProductsClients { get; set; } = new HashSet<ProductClient>();
    }
}
