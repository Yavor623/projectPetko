using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Resources
{
    public class EditResourceViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public int CategoryId { get; set; }

    }
}
