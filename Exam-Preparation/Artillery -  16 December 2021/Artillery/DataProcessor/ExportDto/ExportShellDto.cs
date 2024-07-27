namespace Artillery.DataProcessor.ExportDto
{
    public class ExportShellDto
    {
        public double ShellWeight { get; set; }
        public string Caliber { get; set; } = null!;
        public ExportShellGunDto[] Guns { get; set; }= null!;
    }
}
