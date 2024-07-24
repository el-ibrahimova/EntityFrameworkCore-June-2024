using Newtonsoft.Json;

namespace Invoices.DataProcessor.ExportDto
{
    public class ExportProductWithMostClientsDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

       public string Category { get; set; } = null!;

      public ExportClientsDto[] Clients { get; set; } = null!;
    }
}
