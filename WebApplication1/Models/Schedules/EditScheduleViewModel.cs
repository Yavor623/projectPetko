using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Schedules
{
    public class EditScheduleViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [Display(Name = "Upload Image")]
        public byte[] ByteImage { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
