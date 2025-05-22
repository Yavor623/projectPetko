using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.PersonalSchedule
{
    public class DeletePersonScheduleViewModel
    {
		public int Id { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public string Content { get; set; }
		public byte[] Image { get; set; }
		public int CategoryId { get; set; }
	}
}
