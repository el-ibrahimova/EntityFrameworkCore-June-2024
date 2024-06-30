using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace P03_SalesDatabase.Data.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Sales = new HashSet<Sale>();
        }

        [Key]
        public int CustomerId { get; set; }

        [MaxLength(100)] 
        public string Name { get; set; } = null!;


        [Unicode(false)]
        [MaxLength(80)]
        public string Email { get; set; }

        public string CreditCardNumber { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
