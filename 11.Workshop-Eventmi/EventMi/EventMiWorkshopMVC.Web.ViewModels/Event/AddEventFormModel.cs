using System.ComponentModel.DataAnnotations;

namespace EventMiWorkshopMVC.Web.ViewModels.Event
{
   using static Common.EntityConstraints;
    public class AddEventFormModel
    {
        [Required]
        [StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
         public string StartDate { get; set; } = null!;
       
         [Required]
         public string EndDate { get; set; } = null!;

        [Required]
        [StringLength(EventPlaceMaxLength, MinimumLength = EventPlaceMinLength)]
         public string Place { get; set; } = null!;  
    }
}
