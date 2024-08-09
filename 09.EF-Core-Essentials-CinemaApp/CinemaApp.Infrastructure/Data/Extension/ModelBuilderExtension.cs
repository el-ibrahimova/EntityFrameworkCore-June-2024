using CinemaApp.Infrastructure.Data.Configuration;
using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Infrastructure.Data.Extension
{
    public static class ModelBuilderExtension
    {
        // Seed is extension method who extends method ModelBuilder. If we don't write this for ModelBuilder this method will not be Extension Method
        // => we must write (this ModelBuilder modelBuilder) for Seed method to be Extension
        // по този начин можем да извикаваме метода като вграден такъв => modelBuilder.Seed();

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CinemaConfiguration());

            modelBuilder.ApplyConfiguration(new HallConfiguration());

            modelBuilder.ApplyConfiguration(new MovieConfiguration());

            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new TariffConfiguration());

            modelBuilder.ApplyConfiguration(new SeatConfiguration());
            
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
        }
    }
}
