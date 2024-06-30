using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MusicHub.Data.Models.Enums;

namespace MusicHub.Data.Models
{
    public class Song
    {
        public Song()
        {
            this.SongPerformers = new HashSet<SongPerformer>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.SongNameMaxLength)]
        public string Name { get; set; } = null!;

        // TimeSpan ts = new TimeSpan(1,5,55)  => 01:05:55 = part of time
        // in the DB this will be stored as BIGINT <=> Ticks count
        // by default is required
        public TimeSpan Duration { get; set; }

        
        // by default is required
        public DateTime CreatedOn { get; set; }

        // enums are stored in the DB as INT
        // required by default
        public Genre Genre { get; set; }


         [ForeignKey(nameof(Album))]
        public int? AlbumId { get; set; }

        public virtual Album? Album { get; set; }
        
        [ForeignKey(nameof(Writer))]
        public int WriterId { get; set; }
        public virtual Writer Writer { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<SongPerformer> SongPerformers { get; set; }
    }
}
