using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Invoices.Data.Models;
using Invoices.Data.Models.Enums;
using Invoices.DataProcessor.ImportDto;
using Newtonsoft.Json;

namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using Invoices.Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";


        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Clients");
            XmlSerializer serializier = new XmlSerializer(typeof(ImportClientsDto[]), root);

            using StringReader reader = new StringReader(xmlString);
            var clientsDtos = (ImportClientsDto[])serializier.Deserialize(reader);

            List<Client> validClients = new();
            StringBuilder sb = new();


            foreach (var clientDto in clientsDtos)
            {
                if (!IsValid(clientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client newClient = new Client()
                {
                    Name = clientDto.Name,
                    NumberVat = clientDto.NumberVat
                };

                foreach (var addr in clientDto.Addresses)
                {
                    if (!IsValid(addr))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    newClient.Addresses.Add(new Address()
                    {
                        StreetName = addr.StreetName,
                        StreetNumber = addr.StreetNumber,
                        PostCode = addr.PostCode,
                        City = addr.City,
                        Country = addr.Country
                    });

                }
                validClients.Add(newClient);
                sb.AppendLine(String.Format(SuccessfullyImportedClients, newClient.Name));
            }

            context.Clients.AddRange(validClients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            var invoicesDtos = JsonConvert.DeserializeObject<ImportInvoicesDto[]>(jsonString);

            List<Invoice> validInvoices = new();
            StringBuilder sb = new();


            var validClientIds = context.Clients
                  .Select(c => c.Id)
                  .ToArray();

            foreach (var invoiceDto in invoicesDtos)
            {
                if (!IsValid(invoiceDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime startDate = DateTime.ParseExact(invoiceDto.IssueDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(invoiceDto.DueDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

                // check for start and end date to be right
                if (startDate >= endDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                // check for ClientId to be valid
                if (!validClientIds.Contains(invoiceDto.ClientId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Invoice newInvoice = new Invoice()
                {
                    Number = invoiceDto.Number,
                    IssueDate = startDate,
                    DueDate = endDate,
                    Amount = invoiceDto.Amount,
                    CurrencyType = (CurrencyType)invoiceDto.CurrencyType,
                    ClientId = invoiceDto.ClientId
                };

                validInvoices.Add(newInvoice);
                sb.AppendLine(string.Format(SuccessfullyImportedInvoices, newInvoice.Number));
            }
            context.Invoices.AddRange(validInvoices);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {
            var productsDtos = JsonConvert.DeserializeObject<ImportProductsDto[]>(jsonString);

            List<Product> validProducts = new();
            StringBuilder sb = new();

            foreach (var productDto in productsDtos)
            {
                if (!IsValid(productDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Product newProduct = new Product()
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    CategoryType = (CategoryType)productDto.CategoryType,
                };

                foreach (var clientId in productDto.Clients.Distinct())
                {
                    Client newClient = context.Clients.Find(clientId);

                    if (newClient==null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    newProduct.ProductsClients.Add(new ProductClient()
                    {
                        Client = newClient
                    });
                }

                validProducts.Add(newProduct);
                sb.AppendLine(string.Format(SuccessfullyImportedProducts, newProduct.Name, newProduct.ProductsClients.Count));
            }

            context.Products.AddRange(validProducts);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

    public static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }
} 
}
