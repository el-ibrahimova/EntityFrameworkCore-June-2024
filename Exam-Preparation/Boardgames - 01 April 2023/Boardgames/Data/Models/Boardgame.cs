using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Boardgames.Common;
using Boardgames.Data.Models.Enums;
using Microsoft.EntityFrameworkCore.Storage;
using static Boardgames.Common.ValidationConstants;

namespace Boardgames.Data.Models
{
    public class Boardgame
    {
        public Boardgame()
        {
            BoardgamesSellers = new HashSet<BoardgameSeller>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BoardgameNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public double Rating { get; set; }

        [Required]
        public int YearPublished { get; set; }
       
        [Required]
        public CategoryType CategoryType { get; set; }

        [Required]
        public string Mechanics { get; set; }

        [Required]
        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }
        public virtual Creator Creator {get; set;}

       public virtual ICollection<BoardgameSeller> BoardgamesSellers {get; set;}
    }
}
