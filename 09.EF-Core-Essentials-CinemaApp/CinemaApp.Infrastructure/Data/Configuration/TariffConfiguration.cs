
using System.Text.Json;
using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    internal class TariffConfiguration:IEntityTypeConfiguration<Tariff>
    {
        public void Configure(EntityTypeBuilder<Tariff> builder)
        {

            // // it is good practice to write the path this way = > to be sure that different operation system will read it correctly
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "tariffs.json");
            string data = File.ReadAllText(path);

            var tariffs = JsonSerializer.Deserialize<List<Tariff>>(data);

            if (tariffs != null)
            {
                builder.HasData(tariffs);
            }
        }
    }
}
