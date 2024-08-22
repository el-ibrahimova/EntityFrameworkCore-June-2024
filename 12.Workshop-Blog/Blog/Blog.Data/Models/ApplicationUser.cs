using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser()
        {
            Articles = new List<Article>();
        }
        public ICollection<Article> Articles { get; set; }
    }
}
