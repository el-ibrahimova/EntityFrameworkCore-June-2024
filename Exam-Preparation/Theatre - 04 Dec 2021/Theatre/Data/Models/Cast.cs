﻿ using System.ComponentModel.DataAnnotations;
 using System.ComponentModel.DataAnnotations.Schema;

 namespace Theatre.Data.Models
{
    public class Cast
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        [MaxLength(30)]
        public string FullName { get; set; } = null!;

        [Required]
        public bool IsMainCharacter { get; set; }

        [Required]
        [MaxLength(15)] 
        public string PhoneNumber { get; set; } = null!;

        [ForeignKey(nameof(Play))]
        public int PlayId { get; set; }

        public virtual Play Play { get; set; } = null!;
    }
}
