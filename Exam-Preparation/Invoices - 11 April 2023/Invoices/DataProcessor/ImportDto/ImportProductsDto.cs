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
        [Range(typeof(decimal),ProductPriceMinValue, ProductPriceMaxValue)]  // cast the string to type decimal, because we cannot use decimal as a type in validationConstants
        public decimal Price { get; set; }

        [Required]
        [Range(ProductCategoryTypeMinValue, ProductCategoryTypeMaxValue)]
        public int CategoryType { get; set; }
        
        [Required]
        public int[] Clients { get; set; } = null!;


    }
}
