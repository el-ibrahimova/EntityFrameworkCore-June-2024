using System.Text.Json;
using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // it is good practice to write the path this way = > to be sure that different operation system will read it correctly
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "users.json");
            string data = File.ReadAllText(path);

            var users = JsonSerializer.Deserialize<List<User>>(data);

            if (users != null)
            {
                builder.HasData(users);
            }
        }
    }
}
