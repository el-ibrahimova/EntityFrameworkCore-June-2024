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
            
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {

        }
    }
}


