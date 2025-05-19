using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Resource> Resources { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }

    }
}
