﻿using System.Text.Json;
using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    internal class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            // it is good practice to write the path this way = > to be sure that different operation system will read it correctly
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "halls.json");
            string data = File.ReadAllText(path);

            var halls = JsonSerializer.Deserialize<List<Hall>>(data);

            if (halls != null)
            {
                builder.HasData(halls);
            }
        }
    }
}
