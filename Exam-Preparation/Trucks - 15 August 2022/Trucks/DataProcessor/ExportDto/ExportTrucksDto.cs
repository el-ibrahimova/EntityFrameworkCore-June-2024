using Newtonsoft.Json;

namespace Trucks.DataProcessor.ExportDto
{
    public class ExportTrucksDto
    {
        [JsonProperty("TruckRegistrationNumber")]
        public string RegistrationNumber { get; set; } = null!;

        public string VinNumber { get; set; } = null!;

        public int TankCapacity { get; set; }
        public int CargoCapacity { get; set; }
         public string CategoryType { get; set; } = null!;
         public string MakeType { get; set; }
    }
}
