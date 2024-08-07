using CinemaApp.Data.Configuration;
using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Extention
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CinemaConfiguration());

            modelBuilder.ApplyConfiguration(new HallConfiguration());
        }
    }
}
