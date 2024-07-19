using System.Xml.Serialization;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Patient")]
    public class ExportPatientsWithMedicinesDto
    {
        [XmlAttribute("Gender")] 
        public string Gender { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string AgeGroup { get; set; } = null!;

        [XmlArray("Medicines")] 
        public List<ExportMedicinesBestBeforeDto> Medicines { get; set; } = null!;
    }
}
