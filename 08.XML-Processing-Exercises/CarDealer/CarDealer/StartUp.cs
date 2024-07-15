using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext dbContext = new CarDealerContext();

            // 09
            // string suppliersXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            // Console.WriteLine(ImportSuppliers(dbContext, suppliersXml));

            // 10
            // string partsXml = File.ReadAllText("../../../Datasets/parts.xml");
            // Console.WriteLine(ImportParts(dbContext, partsXml));

            // 11
            // string carsXml = File.ReadAllText("../../../Datasets/cars.xml");
            // Console.WriteLine(ImportCars(dbContext, carsXml));

            // 12
            // string customersXml = File.ReadAllText("../../../Datasets/customers.xml");
            // Console.WriteLine(ImportCustomers(dbContext, customersXml));

            // 13
            // string salesXml = File.ReadAllText("../../../Datasets/sales.xml");
            // Console.WriteLine(ImportSales(dbContext, salesXml));

            // 14
            // Console.WriteLine(GetCarsWithDistance(dbContext));

            // 15
            // Console.WriteLine(GetCarsFromMakeBmw(dbContext));

            // 16
            // Console.WriteLine(GetLocalSuppliers(dbContext));

            // 17
            // Console.WriteLine(GetCarsWithTheirListOfParts(dbContext));

            // 18
            // Console.WriteLine(GetTotalSalesByCustomer(dbContext));

            // 19
            Console.WriteLine(GetSalesWithAppliedDiscount(dbContext));
        }

        // 09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportSuppliersDto[]), new XmlRootAttribute("Suppliers"));

            ImportSuppliersDto[] suppliersDtos;

            using (var reader = new StringReader(inputXml))
            {
                suppliersDtos = (ImportSuppliersDto[])xmlSerializer.Deserialize(reader);
            }

            Supplier[] suppliers = suppliersDtos
                .Select(dto => new Supplier()
                {
                    Name = dto.Name,
                    IsImporter = dto.IsImporter
                })
                .ToArray();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }

        // 10. Ipmort Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportPartsDto[]), new XmlRootAttribute("Parts"));

            ImportPartsDto[] partsDtos;

            using (StringReader inReader = new StringReader(inputXml))
            {
                partsDtos = (ImportPartsDto[])xmlSerializer.Deserialize(inReader);
            }

            // validate supplierId

            var supplierId = context.Suppliers
                .Select(s => s.Id)
                .ToArray();

            var partsWithValidSuppliers = partsDtos
                .Where(p => supplierId.Contains(p.SupplierId))
                .ToArray();

            var validParts = partsWithValidSuppliers
                .Select(dto => new Part()
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Quantity = dto.Quantity,
                    SupplierId = dto.SupplierId
                })
                .ToArray();

            context.Parts.AddRange(validParts);
            context.SaveChanges();

            return $"Successfully imported {validParts.Length}";
        }

        // 11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportCarsDto[]), new XmlRootAttribute("Cars"));

            ImportCarsDto[] carsDtos;

            using (StringReader reader = new StringReader(inputXml))
            {
                carsDtos = (ImportCarsDto[])xmlSerializer.Deserialize(reader);
            };

            List<Car> cars = new();

            foreach (var dto in carsDtos)
            {
                Car car = new Car()
                {
                    Make = dto.Make,
                    Model = dto.Model,
                    TraveledDistance = dto.TraveledDistance
                };

                int[] carPartsId = dto.PartIds
                    .Select(p => p.Id)
                    .Distinct()
                    .ToArray();

                var carParts = new List<PartCar>();

                foreach (var id in carPartsId)
                {
                    carParts.Add(new PartCar()
                    {
                        Car = car,
                        PartId = id
                    });
                }

                car.PartsCars = carParts;
                cars.Add(car);
            }

            context.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        // 12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportCustomersDto[]), new XmlRootAttribute("Customers"));

            ImportCustomersDto[] customersDtos;

            using (StringReader reader = new StringReader(inputXml))
            {
                customersDtos = (ImportCustomersDto[])xmlSerializer.Deserialize(reader);
            }

            var customers = customersDtos
                .Select(dto => new Customer()
                {
                    Name = dto.Name,
                    BirthDate = dto.BirthDate,
                    IsYoungDriver = dto.IsYoungDriver
                })
                .ToArray();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}";
        }

        // 13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportSalesDto[]), new XmlRootAttribute("Sales"));

            ImportSalesDto[] salesDtos;

            using (StringReader reader = new StringReader(inputXml))
            {
                salesDtos = (ImportSalesDto[])xmlSerializer.Deserialize(reader);
            }

            int[] validCarIds = context.Cars
                .Select(c => c.Id)
                .ToArray();

            var validSalesImport = salesDtos
                .Where(dto => validCarIds.Contains(dto.CarId))
                .ToArray();

            Sale[] sales = validSalesImport
                .Select(vs => new Sale()
                {
                    CarId = vs.CarId,
                    CustomerId = vs.CustomerId,
                    Discount = vs.Discount

                })
                .ToArray();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Length}";
        }

        // 14. Export Cars With Distance
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var carWithDistance = context.Cars
                .Select(c => new ExportCarsWithDistanceDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance
                })
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ToArray();

            return SerializeToXml(carWithDistance, "cars");
        }

        // 15. Export Cars from Make BMW
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var bmw = context.Cars
                .Where(c => c.Make == "BMW")
                .Select(c => new ExportCarsFromMakeDto()
                {
                    Id = c.Id,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .ToArray();

            return SerializeToXml(bmw, "cars", true);
        }

        // 16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var localSuppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new LocalSuppliersDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            return SerializeToXml(localSuppliers, "suppliers");
        }

        // 17. Export Cars with Their List of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsWithParts = context.Cars
                .OrderByDescending(c => c.TraveledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .Select(c => new CarWithPartsDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance,
                    Parts = c.PartsCars
                        .OrderByDescending(p => p.Part.Price)
                        .Select(pc => new PartsForCarsDto()
                        {
                            Name = pc.Part.Name,
                            Price = pc.Part.Price
                        })
                        .ToArray()
                })
                .ToArray();

            return SerializeToXml(carsWithParts, "cars");
        }

        // 18. Export Total Sales by Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var temp = context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SalesInfo = c.Sales
                        .Select(s => new
                        {
                            Prices = c.IsYoungDriver
                                ? s.Car.PartsCars.Sum(pc => Math.Round((double)pc.Part.Price * 0.95, 2))
                                : s.Car.PartsCars.Sum(pc => (double)pc.Part.Price)
                        })
                        .ToArray()
                }).ToArray();

            var customerSalesInfo = temp
                .OrderByDescending(x => x.SalesInfo.Sum(y => y.Prices))
                .Select(a => new ExportSalesByCustomerDto()
                {
                    Name = a.FullName,
                    BoughtCars = a.BoughtCars,
                    SpentMoney = a.SalesInfo.Sum(b => (decimal)b.Prices)
                })
                .ToArray();
                
            return SerializeToXml(customerSalesInfo, "customers");
        }

        // 19. Export Sales with Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new ExportSalesWithDiscountDto()
                {
                    Car = new CarDto
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TraveledDistance = s.Car.TraveledDistance
                    },
                    Discount = s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartsCars.Sum(c => c.Part.Price),
                    PriceWithDiscount =
                        Math.Round(s.Car.PartsCars.Sum(c => (double)(c.Part.Price)) * (double)(1 - s.Discount / 100), 4)
                })
                .ToArray();

            return SerializeToXml(sales, "sales");
        }
        

        public static string SerializeToXml<T>(T obj, string rootName, bool omitXmlDeclaration = false)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Object to serialize cannot be null.");

            if (string.IsNullOrEmpty(rootName))
                throw new ArgumentNullException(nameof(rootName), "Root name cannot be null or empty.");

            try
            {
                XmlRootAttribute xmlRoot = new(rootName);
                XmlSerializer xmlSerializer = new(typeof(T), xmlRoot);

                XmlSerializerNamespaces namespaces = new();
                namespaces.Add(string.Empty, string.Empty);

                XmlWriterSettings settings = new()
                {
                    OmitXmlDeclaration = omitXmlDeclaration,
                    Indent = true
                };

                StringBuilder sb = new();
                using var stringWriter = new StringWriter(sb);
                using var xmlWriter = XmlWriter.Create(stringWriter, settings);

                xmlSerializer.Serialize(xmlWriter, obj, namespaces);
                return sb.ToString().TrimEnd();
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine($"Serialization error: {ex.Message}");
                throw new InvalidOperationException($"Serializing {typeof(T)} failed.", ex);
            }
        }

    }
}