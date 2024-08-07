using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder
                .HasData(
                    new Hall()
                    {
                        Id = 1,
                        Name = "IMAX Hall 1",
                        CinemaId = 1
                    },
                    new Hall()
                    {
                        Id = 2,
                        Name = "IMAX Hall 2",
                        CinemaId = 1
                    },
                    new Hall()
                    {
                        Id = 3,
                        Name = "VIP Hall",
                        CinemaId = 2
                    },
                    new Hall()
                    {
                        Id = 4,
                        Name = "3D Hall",
                        CinemaId = 2
                    },
                    new Hall()
                    {
                        Id = 5,
                        Name = "IMAX Hall 5",
                        CinemaId = 3
                    }
                    );
        }
    }
}
