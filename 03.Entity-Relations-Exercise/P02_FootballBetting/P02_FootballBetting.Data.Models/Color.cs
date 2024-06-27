using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Data.Common;


namespace P02_FootballBetting.Data.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ColorNameMaxLength)]
        public string Name { get; set; }
    }
}
