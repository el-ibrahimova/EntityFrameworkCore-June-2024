using System.ComponentModel.DataAnnotations;
using Invoices.Common;
using static Invoices.Common.ValidationConstants;

namespace Invoices.DataProcessor.ImportDto
{
    public class ImportInvoicesDto
    {
        [Required] // int is required by default
        [Range(InvoiceNumberMinValue, InvoiceNumberMaxValue)]
        public int Number { get; set; }

        [Required] 
        public string IssueDate { get; set; } = null!; // DateTime -> deserialize as string

        [Required]
        public string DueDate { get; set; } = null!;   // DateTime -> deserialize as string

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [Range(InvoiceCurrencyTypeMinValue, InvoiceCurrencyTypeMaxValue)]
        public int CurrencyType { get; set; }  // Enums -> deserialize as int
      
        [Required]
        public int ClientId { get; set; }

    }
}
