namespace Cadastre.DataProcessor.ExportDtos
{
    public class ExportPropertiesWithOwnersDto
    {
        public string PropertyIdentifier { get; set; } = null!;
        public int Area { get; set; }
        public string Address { get; set; } = null!;
        public string DateOfAcquisition { get; set; } = null!;
        public ExportOwnersDto[] Owners { get; set; }
    }
}
