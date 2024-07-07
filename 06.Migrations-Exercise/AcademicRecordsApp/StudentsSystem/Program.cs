using System.Security.AccessControl;
using StudentsSystem.Data;
using StudentsSystem.Data.Models;
using ResourceType = StudentsSystem.Data.Models.ResourceType;

namespace StudentsSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new StudentSystemDbContext();

            var resource = new Resource()
            {
                Name = "EF Into video",
                Url = "https://softuni.bg/ef/video1",
                ResourceType = ResourceType.Video,
            };

            //  context.Resources.Add(resource);
            // context.SaveChanges();

            var resources = context.Resources.ToArray();

            foreach (var r in resources)
            {
                Console.WriteLine($"{r.Name} {r.ResourceType}");
            }
        }
    }
}

