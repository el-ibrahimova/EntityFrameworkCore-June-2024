using Newtonsoft.Json;

namespace Medicines.DataProcessor.ExportDtos
{
    public class ExportMedicinesDto
    {
        public string Name { get; set; } = null!;
        public string Price { get; set; } = null!;

        [JsonProperty("Pharmacy")] 
        public ExportPharmaciesDto Pharmacy { get; set; } = null!;
    }
}
