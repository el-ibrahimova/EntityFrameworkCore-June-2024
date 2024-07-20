using Newtonsoft.Json;

namespace Invoices.DataProcessor.ExportDto
{
    public class ExportProductWithMostClientsDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        [JsonProperty("Category")] 
        public string Category { get; set; } = null!;

        [JsonProperty("Clients")] 
        public ExportClientsDto[] Clients { get; set; } = null!;
    }
}
