namespace TravelAgency.DataProcessor.ExportDtos
{
    public class ExportHorseRidingCustomersDto
    {
       public string FullName { get; set; }
       public string PhoneNumber { get; set; }
       public ExportHorseRidingToursDto[] Bookings { get; set; }

    }

    public class ExportHorseRidingToursDto
    {
        public string TourPackageName { get; set; }
        public string Date { get; set; }
    }
}
