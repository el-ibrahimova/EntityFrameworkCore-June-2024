using System.ComponentModel.DataAnnotations;
using Boardgames.Common;
using static Boardgames.Common.ValidationConstants;

namespace Boardgames.Data.Models
{
    public class Creator
    {
        public Creator()
        {
            Boardgames = new HashSet<Boardgame>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CreatorNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(CreatorNameMaxLength)]
        public string LastName { get; set; } 

        public virtual ICollection<Boardgame> Boardgames { get; set; } 
    }
}
