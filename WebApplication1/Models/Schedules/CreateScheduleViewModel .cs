using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Schedules
{
    public class CreateScheduleViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
