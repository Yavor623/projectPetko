using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Schedules
{
    public class EditScheduleViewModel
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
        public string ImageUrl { get; set; }
        public Schedule Schedule { get; set; }

    }
}
