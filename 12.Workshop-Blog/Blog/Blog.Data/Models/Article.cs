using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        [MaxLength(4096)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
