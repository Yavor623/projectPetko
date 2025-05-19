using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<ApplicationUserSchedule> ApplicationUserSchedules { get; set; }
        
    }
}
