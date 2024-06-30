﻿using System.ComponentModel.DataAnnotations;

namespace P03_SalesDatabase.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Sales = new HashSet<Sale>();
        }
        [Key]
        public int ProductId { get; set; }

        [MaxLength(50)] public string Name { get; set; } = null!;

        public double Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }


    }
}
