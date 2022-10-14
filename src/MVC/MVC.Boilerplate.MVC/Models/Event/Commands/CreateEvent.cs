using System.ComponentModel.DataAnnotations;

namespace MVC.Boilerplate.Models.Event.Commands
{
    public class CreateEvent
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "price should be greater than zero")]
        public int Price { get; set; }

        [Required]
        public string Artist { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
