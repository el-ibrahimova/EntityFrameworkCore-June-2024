using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.DTOs;
using ProductShop.DTOs.Export;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext dBcontext = new ProductShopContext();

            //  01.
            //  string usersJson = File.ReadAllText("../../../Datasets/users.json");
            //  Console.WriteLine(ImportUsers(dBcontext, usersJson));

            // 02.  
            // string productsJson = File.ReadAllText("../../../Datasets/products.json");
            // Console.WriteLine(ImportProducts(dBcontext, productsJson));

            // 03. 
            // string categoriesJson = File.ReadAllText("../../../Datasets/categories.json");
            // Console.WriteLine(ImportCategories(dBcontext, categoriesJson));

            // 04.
            // string catProdJson = File.ReadAllText("../../../Datasets/categories-products.json");
            // Console.WriteLine(ImportCategoryProducts(dBcontext, catProdJson));

            // 05.
            // Console.WriteLine(GetProductsInRange(dBcontext));

            // 06.
            // Console.WriteLine(GetSoldProducts(dBcontext));

            // 07.
            // Console.WriteLine(GetCategoriesByProductsCount(dBcontext));

            // 08.
            Console.WriteLine(GetUsersWithProducts(dBcontext));
        }


        // 01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }


        // 02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            if (products != null)
            {
                context.Products.AddRange(products);
                context.SaveChanges();
            }

            return $"Successfully imported {products.Length}";
        }


        // 03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var allCategories = JsonConvert.DeserializeObject<Category[]>(inputJson);

            var validCategories = allCategories?
                .Where(c => c.Name != null)
                .ToArray();

            if (validCategories == null)
            {
                return $"Successfully imported 0";
            }

            context.Categories.AddRange(validCategories);
            context.SaveChanges();
            return $"Successfully imported {validCategories.Length}";
        }


        // 04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoriesProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoriesProducts.AddRange(categoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Length}";
        }


        // 05. Export products in range - solution with DTO
        public static string GetProductsInRange(ProductShopContext context)
        {
            var productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ExportProductDto()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .OrderBy(p => p.Price)
                .ToArray();

            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(productsInRange, settings);
            return json;
        }

        // 06. Export Sold Products - solution with and without DTO
        public static string GetSoldProducts(ProductShopContext context)
        {
            // solution without DTO

            //var userWithSoldProducts = context.Users
            //    .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
            //    .OrderBy(u => u.LastName)
            //    .ThenBy(u => u.FirstName)
            //    .Select(u => new
            //    {
            //        firstName = u.FirstName,
            //        lastName = u.LastName,
            //        soldProducts = u.ProductsSold
            //            .Where(b => b.BuyerId != null)
            //            .Select(p => new
            //            {
            //                name = p.Name,
            //                price = p.Price,
            //                buyerFirstName = p.Buyer!.FirstName, // Buyer! = it is sure that Buyer is not null 
            //                buyerLastName = p.Buyer.LastName
            //            }).ToArray()
            //    })
            //    .ToArray();

            //var jsonOutput = JsonConvert.SerializeObject(userWithSoldProducts, Formatting.Indented);
            //return jsonOutput;
            

            // solution with DTO

            var soldProducts = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(u => new SellerWithProductsDto()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                        .Select(p => new SoldProductsDto()
                    {
                        Name = p.Name,
                        Price = p.Price,
                        BuyerFirstName = p.Buyer.FirstName,
                        BuyerLastName = p.Buyer.LastName
                    })
                })
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToArray();

            
                string output = JsonConvert.SerializeObject(soldProducts, Formatting.Indented);
                return output;

        }

        // 07. Export Categories by Products Count

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categoryByCount = context.Categories

                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoriesProducts.Count,
                    averagePrice = c.CategoriesProducts
                        .Average(cp => cp.Product.Price).ToString("f2"),
                    totalRevenue = c.CategoriesProducts
                        .Sum(cp => cp.Product.Price).ToString("f2")
                })
                .OrderByDescending(p => p.productsCount)
                .ToArray();

            var jsonOutput = JsonConvert.SerializeObject(categoryByCount, Formatting.Indented);
            return jsonOutput;
        }

        // 08. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersWithProduct = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = u.ProductsSold
                        .Where(p => p.BuyerId != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price
                        })
                        .ToArray()
                })
                .OrderByDescending(u => u.soldProducts.Count())
                .ToArray();


            var output = new
            {
                usersCount = usersWithProduct.Count(),
                users = usersWithProduct.Select(u => new
                {
                    u.firstName,
                    u.lastName,
                    u.age,
                    soldProducts = new
                    {
                        count = u.soldProducts.Count(),
                        products = u.soldProducts
                    }
                })
            };

            string jsonOutput = JsonConvert.SerializeObject(output, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            return jsonOutput;
        }
    }
}
