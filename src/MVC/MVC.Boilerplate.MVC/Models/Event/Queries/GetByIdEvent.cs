using System.ComponentModel.DataAnnotations;

namespace MVC.Boilerplate.Models.Event.Queries
{
    public class GetByIdEvent
    {
        public string EventId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
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
