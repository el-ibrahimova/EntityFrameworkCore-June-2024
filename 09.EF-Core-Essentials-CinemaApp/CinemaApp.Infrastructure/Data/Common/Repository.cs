using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Infrastructure.Data.Common
{
    public class Repository : IRepository
    {
        protected DbContext Context;

        // constructor injection 
        public Repository(CinemaDbContext dbContext)
        {
            Context = dbContext;
        }

        // this method is not part of the interface IRepository
        protected DbSet<T> DbSet<T>() where T : class
        {
            return Context.Set<T>();
        }

        // methods Task are void = they don't return result
        public async Task AddAsync<T>(T entity) where T : class
        {
            await DbSet<T>()
                .AddAsync(entity);
        }

        // methods Task are void = they don't return result
        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await DbSet<T>()
                .AddRangeAsync(entities);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>();
        }

        public IQueryable<T> AllReadonly<T>() where T : class
        {
            return DbSet<T>()
                .AsNoTracking();
        }

        public void Dispose()
        {
            Context.Dispose();
        }


        public async Task<T?> GetByIdAsync<T>(object id) where T : class
        {
            return await DbSet<T>()
                .FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
