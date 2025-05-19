using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class ApplicationUserSchedule
    {
        public int Id { get; set; }
        [ForeignKey("ScheduleId")]
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
