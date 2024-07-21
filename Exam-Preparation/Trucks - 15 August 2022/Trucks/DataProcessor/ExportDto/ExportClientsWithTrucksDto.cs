using Newtonsoft.Json;

namespace Trucks.DataProcessor.ExportDto
{
    public class ExportClientsWithTrucksDto
    {
        public string Name { get; set; } = null!;
      
        public ExportTrucksDto[] Trucks { get; set; } = null!;

    }
}
