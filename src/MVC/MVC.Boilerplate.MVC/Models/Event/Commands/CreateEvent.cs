using System.ComponentModel.DataAnnotations;

namespace MVC.Boilerplate.Models.Event.Commands
{
    public class CreateEvent
    {
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "price should be greater than zero")]

        public int Price { get; set; }
        public string Artist { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
    }
}
