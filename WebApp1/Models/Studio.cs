using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class Studio
    {
        [Key]
        public int StudioId { get; set; }
        public string StudioName { get; set; }
        public string StudioDescription { get; set; }
        public List<Game> Games { get; set; }
    }
}
