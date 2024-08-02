using System.ComponentModel.DataAnnotations;

namespace TeisterMask.DataProcessor.ImportDto
{
    public class ImportEmployeeDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression(@"^[A-Za-z0-9]{3,}$")]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress] 
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$")]
        public string Phone { get; set; }
        public int[] Tasks { get; set; }
    }
}
