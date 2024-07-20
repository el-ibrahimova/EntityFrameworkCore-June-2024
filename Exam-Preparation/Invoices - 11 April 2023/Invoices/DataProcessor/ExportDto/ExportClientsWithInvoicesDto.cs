using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto
{
    [XmlType("Client")]
    public class ExportClientsWithInvoicesDto
    {
        [XmlAttribute("InvoicesCount")]
        public int InvoicesCount { get; set; }

        [XmlElement("ClientName")] 
        public string ClientName { get; set; } = null!;

        public string VatNumber { get; set; } = null!;

        [XmlArray("Invoices")] 
        public ExportInvoicesDto[] Invoices { get; set; } = null!;
    }
}
