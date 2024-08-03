using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TravelAgency.Data;
using TravelAgency.Data.Models;
using TravelAgency.DataProcessor.ImportDtos;

namespace TravelAgency.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedCustomer = "Successfully imported customer - {0}";
        private const string SuccessfullyImportedBooking = "Successfully imported booking. TourPackage: {0}, Date: {1}";

        public static string ImportCustomers(TravelAgencyContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Customers");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCustomerDto[]), root);

            using StringReader reader = new StringReader(xmlString);
            var customersDtos = (ImportCustomerDto[])serializer.Deserialize(reader);

            List<Customer> validCustomers = new();
            StringBuilder sb = new();

            foreach (var customerDto in customersDtos)
            {
                if (!IsValid(customerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (validCustomers.Any(c => c.FullName == customerDto.FullName)
                    || validCustomers.Any(c => c.Email == customerDto.Email)
                    || validCustomers.Any(c => c.PhoneNumber == customerDto.PhoneNumber))
                {
                    sb.AppendLine(DuplicationDataMessage);
                    continue;
                }

                Customer customer = new Customer()
                {
                    PhoneNumber = customerDto.PhoneNumber,
                    FullName = customerDto.FullName,
                    Email = customerDto.Email
                };

                validCustomers.Add(customer);
                sb.AppendLine(string.Format(SuccessfullyImportedCustomer, customer.FullName));
            }

            context.Customers.AddRange(validCustomers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        public static string ImportBookings(TravelAgencyContext context, string jsonString)
        {

            var bookingsDtos = JsonConvert.DeserializeObject<ImportBookingsDto[]>(jsonString);

            List<Booking> validBookings = new();
            StringBuilder sb = new();

            foreach (var bookingDto in bookingsDtos)
            {
                if (!IsValid(bookingDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime bookingDate;
                bool isValidBookingDate = DateTime.TryParseExact(bookingDto.BookingDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out bookingDate);

                if (!isValidBookingDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }



                Customer customer = context.Customers.FirstOrDefault(c=>c.FullName == bookingDto.CustomerName);
                TourPackage tourPackage = context.TourPackages.FirstOrDefault(b=>b.PackageName==bookingDto.TouPackageName);

                Booking booking = new Booking()
                {
                    BookingDate = bookingDate,
                    Customer = customer,
                    TourPackage = tourPackage
                };

                var name =tourPackage.PackageName;
                var date = booking.BookingDate.ToString("yyyy-MM-dd");

                validBookings.Add(booking);
                sb.AppendLine(string.Format(SuccessfullyImportedBooking, name,date ));
            }

            context.Bookings.AddRange(validBookings);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            var validateContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validateContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                string currValidationMessage = validationResult.ErrorMessage;
            }

            return isValid;
        }
    }
}
