using System.ComponentModel.DataAnnotations;
using Cadastre.Common;
using Cadastre.Data.Enumerations;
using Newtonsoft.Json;

namespace Cadastre.DataProcessor.ImportDtos
{
    public class ImportCitizensDto
    {
        //"FirstName": "Ivan",
        //"LastName": "Georgiev",
        //"BirthDate": "12-05-1980",
        //"MaritalStatus": "Married",
        //"Properties": [ 17, 29 ]

        [JsonProperty("FirstName")]  
        [Required]
        [MinLength(ValidationConstants.CitizenNameMinLength)]
        [MaxLength(ValidationConstants.CitizenNameMaxLength)]
        public string FirstName { get; set; } = null!;

       
        [JsonProperty("LastName")]
        [Required]
        [MinLength(ValidationConstants.CitizenNameMinLength)]
        [MaxLength(ValidationConstants.CitizenNameMaxLength)]
        public string LastName { get; set; } = null!;

        [JsonProperty("BirthDate")] 
        [Required] 
        public string BirthDate { get; set; } = null!;

        [JsonProperty("MaritalStatus")]
        [Required]
        [EnumDataType(typeof(MaritalStatus))]
        public string MaritalStatus { get; set; } = null!;

        [JsonProperty("Properties")] 
        public int[] Properties { get; set; } = null!;
    }
}
