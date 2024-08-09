using System.Text.Json;
using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    public class CinemaHallsConfiguration : IEntityTypeConfiguration<CinemaHall>
    {
        public void Configure(EntityTypeBuilder<CinemaHall> builder)
        {
            // it is good practice to write the path this way = > to be sure that different operation system will read it correctly
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "cinemasHalls.json");
            string data = File.ReadAllText(path);

            var cinemasHalls = JsonSerializer.Deserialize<List<CinemaHall>>(data);

            if (cinemasHalls != null)
            {
                builder.HasData(cinemasHalls);
            }
        }
    }
}
