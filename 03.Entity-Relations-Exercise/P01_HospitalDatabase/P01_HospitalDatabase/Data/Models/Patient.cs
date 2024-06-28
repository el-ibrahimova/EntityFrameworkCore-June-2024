using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }


        [MaxLength(50)] 
        public string FirstName { get; set; } = null!;


        [MaxLength(50)]
        public string LastName { get; set; } = null!;


        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(80)]
        public string Email { get; set; }

        public bool HasInsurance { get; set; }
    }
}
