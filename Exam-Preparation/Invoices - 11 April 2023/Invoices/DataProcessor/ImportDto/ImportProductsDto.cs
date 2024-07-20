using System.ComponentModel.DataAnnotations;
using Invoices.Data.Models;
using static Invoices.Common.ValidationConstants;

namespace Invoices.DataProcessor.ImportDto
{
    public class ImportProductsDto
    {
        [Required]
        [MinLength(ProducNameMinLength)]
        [MaxLength(ProducNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(ProductPriceMinValue, ProductPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(ProductCategoryTypeMinValue, ProductCategoryTypeMaxValue)]
        public int CategoryType { get; set; }

        public int[] Clients { get; set; } = null!;


    }
}
