using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Invoices.Data.Models.Enums;

namespace Invoices.Data.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

       // int is required by default
        public int Number { get; set; }

       // DateTime is required by default
        public DateTime IssueDate { get; set; }

       
        // DateTime is required by default
        public DateTime DueDate { get; set; }

       //decimal is required by default
        public decimal Amount { get; set; }

        // Enumeration is stored in the DB as int = > required by default
        public CurrencyType CurrencyType { get; set; }

       
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
       public Client Client { get; set; } = null!;
    }
}
