namespace CinemaApp.Data.Common
{
    public interface IRepository : IDisposable
    {
        // we don't have to make repository of type <T> generic.
        // We make only the methods of type <T> generic, because it is possible to work with different tables with different types of data

        IQueryable<T> All<T>() where T : class;

        IQueryable<T> AllReadonly<T>() where T : class;

        Task<T?> GetByIdAsync<T>(object id) where T : class;

        Task AddAsync<T>(T entity) where T : class;

        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;

        Task<int> SaveChangesAsync(); // int - how many changes are saved


        // Task - от асинхронно програмиране - Async await - задачата ще се изпълни, но не сега, а когато EF прецени (това зависи от много неща)
        // => задачата ще се извърши асинхронно
    }
}
