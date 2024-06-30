using System.ComponentModel.DataAnnotations;

namespace P03_SalesDatabase.Data.Models
{
    public class Store
    {
        public Store()
        {
            this.Sales = new HashSet<Sale>();
        }

        [Key]
        public int StoreId { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
