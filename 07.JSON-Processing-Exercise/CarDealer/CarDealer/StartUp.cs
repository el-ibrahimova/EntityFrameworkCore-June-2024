using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext dBcontext = new CarDealerContext();

            // 09
            // string jsonSuppliers = File.ReadAllText("../../../Datasets/suppliers.json");
            // Console.WriteLine(ImportSuppliers(dBcontext,jsonSuppliers));

            // 10
            // string jsonParts = File.ReadAllText("../../../Datasets/parts.json");
            // Console.WriteLine(ImportParts(dBcontext, jsonParts));

            // 11
            // string jsonCars = File.ReadAllText("../../../Datasets/cars.json");
            // Console.WriteLine(ImportCars(dBcontext, jsonCars));

            // 12
            // string jsonCustomers = File.ReadAllText("../../../Datasets/customers.json");
            // Console.WriteLine(ImportCustomers(dBcontext, jsonCustomers));

            // 13
            // string jsonSales = File.ReadAllText("../../../Datasets/sales.json");
            // Console.WriteLine(ImportSales(dBcontext, jsonSales));

            // 14
            // Console.WriteLine(GetOrderedCustomers(dBcontext));

            // 15
            // Console.WriteLine(GetCarsFromMakeToyota(dBcontext));

            // 16
            // Console.WriteLine(GetLocalSuppliers(dBcontext));

            // 17
            // Console.WriteLine(GetCarsWithTheirListOfParts(dBcontext));

            // 18
            // Console.WriteLine(GetTotalSalesByCustomer(dBcontext));

            // 19
            Console.WriteLine(GetSalesWithAppliedDiscount(dBcontext));

        }

        // 09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}.";
        }

        // 10. Import parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<Part[]>(inputJson);

            int[] supplierIds = context.Suppliers.Select(x => x.Id).ToArray();

            var validParts = parts.Where(p => supplierIds.Contains(p.SupplierId)).ToArray();

            context.Parts.AddRange(validParts);
            context.SaveChanges();

            return $"Successfully imported {validParts.Count()}.";
        }

        // 11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsDtos = JsonConvert.DeserializeObject<ImportCarDto[]>(inputJson);

            var cars = new HashSet<Car>();
            var partsCars = new HashSet<PartCar>();

            foreach (var carDto in carsDtos)
            {
                var newCar = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TraveledDistance = carDto.TraveledDistance
                };

                cars.Add(newCar);

                foreach (var partId in carDto.PartsId.Distinct()) // Distinct = only unique values
                {
                    partsCars.Add(new PartCar()
                    {
                        Car = newCar,
                        PartId = partId
                    });
                }
            }

            context.Cars.AddRange(cars);
            context.PartsCars.AddRange(partsCars);

            context.SaveChanges();

            return $"Successfully imported {cars.Count()}.";

        }

        // 12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count()}.";
        }

        // 13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count()}.";
        }

        // 14. Export Ordered Customers
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var orderedCustomers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver.Equals(0))
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    c.IsYoungDriver
                })
                .ToArray();

            string outputJson = JsonConvert.SerializeObject(orderedCustomers, Formatting.Indented);
            return outputJson;
        }

        // 15. Export Cars from Make Toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var toyotaCars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TraveledDistance
                })
                .ToArray();

            string outputJson = JsonConvert.SerializeObject(toyotaCars, Formatting.Indented);
            return outputJson;
        }

        // 16. Export local suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var localSuppliers = context.Suppliers
               .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            var outputJson = JsonConvert.SerializeObject(localSuppliers, Formatting.Indented);
            return outputJson;
        }

        // 17. Export cars with their list of parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsWithParts = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TraveledDistance
                    },
                    parts = c.PartsCars
                        .Select(p => new
                        {
                            p.Part.Name,
                            Price = p.Part.Price.ToString("f2")
                        })
                })
                .ToArray();

            string outputJson = JsonConvert.SerializeObject(carsWithParts, Formatting.Indented);
            return outputJson;
        }

        // 18. Export Total Sales by Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customerTotalSales = context.Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new CustomerTotalSalesDto()
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales.SelectMany(s=>s.Car.PartsCars).Sum(pc=>pc.Part.Price)
                })
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCars)
                .ToArray();
            

            string jsonOutput = JsonConvert.SerializeObject(customerTotalSales, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });
            
            return jsonOutput;
        }

        // 19. Export Sales with Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var salesWithDiscount = context.Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TraveledDistance
                    },
                    customerName = s.Customer.Name,
                    discount = s.Discount.ToString("f2"),
                    price = s.Car.PartsCars.Sum(pc=>pc.Part.Price).ToString("f2"),
                    priceWithDiscount = (s.Car.PartsCars.Sum(pc => pc.Part.Price)* (1-s.Discount/100)).ToString("f2")
                })
                .ToArray();

            string jsonOutput = JsonConvert.SerializeObject(salesWithDiscount, Formatting.Indented);
            return jsonOutput;
        }
    }
}