using System.Xml.Serialization;
using Artillery.Data.Models;

namespace Artillery.DataProcessor.ExportDto
{
    [XmlType(nameof(Gun))]
    public class ExportGunsWithCountriesDto
    {
        [XmlAttribute(nameof(Manufacturer))] public string Manufacturer { get; set; } = null!;

        [XmlAttribute(nameof(GunType))]
        public string GunType { get; set; }


        [XmlAttribute(nameof(GunWeight))]
        public int GunWeight { get; set; }

        [XmlAttribute(nameof(BarrelLength))]
        public double BarrelLength { get; set; }

        [XmlAttribute(nameof(Range))]
        public int Range { get; set; }


        [XmlArray(nameof(Countries))] public ExportCountriesDto[] Countries { get; set; } = null!;
    }
}
