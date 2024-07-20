using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto
{
    [XmlType("Invoice")]
    public  class ExportInvoicesDto
    {
        [XmlElement("InvoiceNumber")] 
        public int InvoiceNumber { get; set; }

        [XmlElement("InvoiceAmount")]
        public decimal Amount { get; set; }

        public string DueDate { get; set; } = null!;

        [XmlElement("Currency")]
        public string Currency { get; set; } = null!;
    }
}
