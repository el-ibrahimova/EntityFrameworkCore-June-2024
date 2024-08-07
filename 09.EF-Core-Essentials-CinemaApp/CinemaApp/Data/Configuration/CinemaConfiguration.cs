using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CinemaApp.Data.Configuration
{
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder
                .HasData(
                      new Cinema()
                    {
                        Id = 1,
                        Name = "Arena Mladost",
                        Address = "Mladost 4, Sofia"
                    }, new Cinema()
                    {
                        Id = 2,
                        Name = "Arena Stara Zagora",
                        Address = "Stara Zaroga Mall"
                    }, new Cinema()
                    {
                        Id = 3,
                        Name = "Cinema City",
                        Address = "Mall of Sofia"
                    });
        }
    }
}
