using System.ComponentModel.DataAnnotations;
using Invoices.Common;
using Invoices.Data.Models.Enums;

namespace Invoices.Data.Models
{
    public class Product
    {
        [Key] public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ProducNameMaxLength)]
        public string Name { get; set; } = null!;

        // decimal is required by default
        public decimal Price { get; set; }

        // Enumeration is stored in the DB as int = > required by default
        public CategoryType CategoryType { get; set; }

        public virtual ICollection<ProductClient> ProductsClients { get; set; } = new HashSet<ProductClient>();
    }
}
