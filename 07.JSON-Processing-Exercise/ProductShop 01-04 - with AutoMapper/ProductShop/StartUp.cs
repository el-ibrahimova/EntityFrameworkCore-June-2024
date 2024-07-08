using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext dbContext = new ProductShopContext();

            // 01 string inputJson = File.ReadAllText(@"../../../Datasets/users.json");
            //    string result = ImportUsers(dbContext, inputJson);

            // 02 string inputJson = File.ReadAllText(@"../../../Datasets/products.json");
            //    string result = ImportProducts(dbContext, inputJson);

            // 03 string inputJson = File.ReadAllText(@"../../../Datasets/categories.json");
            //    string result = ImportCategories(dbContext, inputJson);

            string inputJson = File.ReadAllText(@"../../../Datasets/categories-products.json");
            string result = ImportCategoryProducts(dbContext, inputJson);
            Console.WriteLine(result);
        }

        // 01. Import Data
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            }));


            ImportUserDto[] userDtos = JsonConvert.DeserializeObject<ImportUserDto[]>(inputJson);

            // AutoMapper can map collections also
            // In case of no validation you can:
            // User[] users = mapper.Map<User[]>userDtos);


            // This way allows you additional validations
            ICollection<User> validUsers = new HashSet<User>();

            foreach (var userDto in userDtos)
            {
                User user = mapper.Map<User>(userDto);
                validUsers.Add(user);
            }
            // Here we have all valid users ready for the DB

            context.Users.AddRange(validUsers);
            context.SaveChanges();

            return $"Successfully imported {validUsers.Count}";
        }

        // 02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            }));

            var productDtos = JsonConvert.DeserializeObject<ImportProductDto[]>(inputJson);

            ICollection<Product> validProducts = new HashSet<Product>();

            foreach (var productDto in productDtos)
            {
                Product product = mapper.Map<Product>(productDto);
                validProducts.Add(product);
            }

            context.AddRange(validProducts);
            context.SaveChanges();

            return $"Successfully imported {validProducts.Count}";
        }

        // 03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            }));

            // In this collection of DTOs, there can be invalid entries
            var categoriesDtos = JsonConvert.DeserializeObject<ImportCategoryDto[]>(inputJson);

            ICollection<Category> validCategories = new HashSet<Category>();

            foreach (var categoryDto in categoriesDtos)
            {
                if (string.IsNullOrEmpty(categoryDto.Name))
                {
                    // we skip this entry
                    continue;
                }

                Category category = mapper.Map<Category>(categoryDto);
                validCategories.Add(category);
            }

            context.AddRange(validCategories);
            context.SaveChanges();

            return $"Successfully imported {validCategories.Count}";
        }

        // 04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            }));

            var categoryProductDto = JsonConvert.DeserializeObject<ImportCategoryProductDto[]>(inputJson);

            ICollection<CategoryProduct> validEntries = new HashSet<CategoryProduct>();

            foreach (var entry in categoryProductDto)
            {
                // check if such of entry exist in Entities Categories and Products
                //// this is not from description, but in real project it is good to check
                
                if (!context.Categories.Any(c => c.Id == entry.CategoryId)
                   || !context.Products.Any(p => p.Id == entry.ProductId))
                {
                 // if doesnt exist skip this entry
                   continue;
                }

                CategoryProduct cProduct = mapper.Map<CategoryProduct>(entry);
                validEntries.Add(cProduct);
            }

            context.CategoriesProducts.AddRange(validEntries);
            context.SaveChanges();

            return $"Successfully imported {validEntries.Count}";
        }
    }
}