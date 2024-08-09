using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    internal class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            // it is good practice to write the path this way = > to be sure that different operation system will read it correctly
           string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "cinemas.json");
            string data = File.ReadAllText(path);

            var cinemas = JsonSerializer.Deserialize<List<Cinema>>(data);

            if (cinemas != null)
            {
                builder.HasData(cinemas);
            }
        }
    }
}
