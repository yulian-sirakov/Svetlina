using System.ComponentModel.DataAnnotations;

namespace Svetlina.Data.Models
{
    public class ScheduleWorkDay
    {
        [Key]
        public int Id { get; set; }
        public Project project { get; set; }
        public DateTime Day { get; set; }
        public ScheduleWorkDay()
        {
                Day = DateTime.Now;
        }
    }
}
