using System.ComponentModel.DataAnnotations;

namespace RatingApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Please insert valid value (between 1 to 5)")]
        public int Rating { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Oh, unfortunatly your name is too long")]
        public string Name { get; set; }
        [StringLength(200, ErrorMessage = "Oh, unfortunatly your review is too long")]
        public string Opinion { get; set; }

        public string Time { get; set; }
    }
}
