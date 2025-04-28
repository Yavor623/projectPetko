using System.ComponentModel.DataAnnotations;

namespace Svoboda.Models.Schedules
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
        public string ImageUrl { get; set; }
    }
}
