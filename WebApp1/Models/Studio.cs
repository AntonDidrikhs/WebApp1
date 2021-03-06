using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace WebApp1.Models
{
    public class Studio
    {
        [Key]
        public int StudioId { get; set; }
        [Display(Name = "Studio's Name")]
        public string StudioName { get; set; }
        [Display(Name = "Description")]
        public string StudioDescription { get; set; }
        [ValidateNever]
        public List<Game> Games { get; set; }
    }
}
