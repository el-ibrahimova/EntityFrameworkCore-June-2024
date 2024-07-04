using System.Globalization;
using System.Text;
using BookShop.Models;
using BookShop.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            // AsNoTracking() -> Detach collection/entity from the Change Tracker
            // Any changes made will not be saved

            // ToArray()/ToList() -> Materialize the query
            // Any code that we write later will not be executed in the DB as SQL
            // The code after materialization is executed locally on the machine in RAM

              using var dbContext = new BookShopContext();
           //   DbInitializer.ResetDatabase(dbContext);

            // 02. string result = GetBooksByAgeRestriction(dbContext, "miNor");
            // 03. string result = GetGoldenBooks(dbContext);
            // 04. string result = GetBooksByPrice(dbContext);
            // 05. string result = GetBooksNotReleasedIn(dbContext, 2000);
            // 06. string result = GetBooksByCategory(dbContext, "horror mystery drama");
            // 07. string result = GetBooksReleasedBefore(dbContext, "30-12-1989");
            // 08. string result = GetAuthorNamesEndingIn(dbContext, "e");
            // 09. string result = GetBookTitlesContaining(dbContext, "sK");
            // 10. string result = GetBooksByAuthor(dbContext, "R");

            // 11. int number = int.Parse(Console.ReadLine());
            //  int result = CountBooks(dbContext, number);
            //  Console.WriteLine($"There are {result} books with longer title than {number} symbols");

            // 12. string result = CountCopiesByAuthor(dbContext);
            // 13. string result = GetTotalProfitByCategory(dbContext);
            // 14. string result = GetMostRecentBooks(dbContext);
            // 15. IncreasePrices(dbContext);

           int result =  RemoveBooks(dbContext);
            Console.WriteLine(result);

        }

        // 02. Age Restriction

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            try
            {
                AgeRestriction ageRestriction = Enum.Parse<AgeRestriction>(command, true);

                string[] bookTitles = context.Books
                    .Where(b => b.AgeRestriction == ageRestriction)
                    .OrderBy(b => b.Title)
                    .Select(b => b.Title)
                    .ToArray();

                return string.Join(Environment.NewLine, bookTitles);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // 03. Golden Books

        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenBooks = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, goldenBooks);
        }

        // 04. Books By Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            var booksByPrice = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var b in booksByPrice)
            {
                sb.AppendLine($"{b.Title} - ${b.Price:f2}");
            }

            return sb.ToString().TrimEnd();

        }

        // 05. Not Released In

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var notReleased = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, notReleased);
        }

        // 06. Book Titles By Category

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();

            string[] bookTitles = context.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, bookTitles);
        }

        // 07. Released Before Date

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            // Parse string date to type DateTime
            DateTime dt = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var bookReleased = context.Books
                .Where(b => b.ReleaseDate < dt)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}")
                .ToArray();

            return string.Join(Environment.NewLine, bookReleased);
        }

        // 08. Author Search

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var name = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .Select(a => $"{a.FirstName} {a.LastName}")
                .ToArray();

            return string.Join(Environment.NewLine, name);
        }

        // 09. Book Search

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var bookTitles = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            return string.Join(Environment.NewLine, bookTitles);
        }

        // 10. Book Search By Author

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var booksAndAuthors = context.Books
                .Where(a => a.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(a => $"{a.Title} ({a.Author.FirstName} {a.Author.LastName})")
                .ToArray();

            return string.Join(Environment.NewLine, booksAndAuthors);
        }

        // 11. Count Books

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var countOfBooks = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .ToArray();

            return countOfBooks.Length;
        }

        // 12. Total Books Copies

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var copiesByAuthor = context.Authors
                .Select(a => new
                {
                    AuthorName = $"{a.FirstName} {a.LastName}",
                    BooksCopies = a.Books.Sum(b => b.Copies)
                })
                .ToArray()
                .OrderByDescending(a => a.BooksCopies);
            // in this case query is faster executed when first materialize and then order the result


            StringBuilder sb = new();

            foreach (var a in copiesByAuthor)
            {
                sb.AppendLine($"{a.AuthorName} - {a.BooksCopies}");
            }

            return sb.ToString().TrimEnd();
        }

        // 13. Profit By Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var profitByCategory = context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    Profit = c.CategoryBooks.Sum(b => b.Book.Copies * b.Book.Price)
                })
                .ToArray()               // optimize the code when we materialize the query and then order it
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name);

            StringBuilder sb = new();

            foreach (var c in profitByCategory)
            {
                sb.AppendLine($"{c.Name} ${c.Profit:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        // 14. Most Recent Books

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var recentBooks = context.Categories
                  .OrderBy(c => c.Name)
                  .Select(c => new
                  {
                      CategoryName = c.Name,
                      Recent = c.CategoryBooks
                        .OrderByDescending(cb => cb.Book.ReleaseDate)
                        .Take(3)
                        .Select(cb => new
                        {
                            BookName = cb.Book.Title,
                            ReleaseYear = cb.Book.ReleaseDate.Value.Year
                        })
                        .ToArray()
                  })
                .ToArray();

            StringBuilder sb = new();

            foreach (var c in recentBooks)
            {
                sb.AppendLine($"--{c.CategoryName}");

                foreach (var b in c.Recent)
                {
                    sb.AppendLine($"{b.BookName} ({b.ReleaseYear})");
                }
            }

            return sb.ToString().TrimEnd();


        }

        // 15. Increase Prices

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year < 2010)
                .ToArray();
            // materializing the query does not detach enetities from Change Tracker

            foreach (var book in books)
            {
                book.Price += 5;
            }

            // option 1
            context.SaveChanges();

            // option 2 - with BULK operator
            // in BookShop.Data and BookShop projects we install Z.EntityFramework.Extensions.EFCore - version 6.18.5
            // After version of EF 7 BULK is included 

            // context.BulkUpdate(books);

            // 4454 ms with SaveChanges
            // 3677 ms with BulkUpdate


            // option 3 Using BatchUpdate from EFCore.Extensions

            //context.Books
            //    .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year < 2010)
            //    .UpdateFromQuery(b => new Book() { Price = b.Price + 5 });
        }

        // 16. Remove Books

        public static int RemoveBooks(BookShopContext context)
        {
            var booksToRemove = context.Books
                .Where(b => b.Copies < 4200)
                .ToArray();

          //  context.BulkDelete(booksToRemove);

            context.RemoveRange(booksToRemove);
            context.SaveChanges();

            return booksToRemove.Length;
        }

    }
}



