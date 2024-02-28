using System.ComponentModel.DataAnnotations;

namespace Svetlina.Data.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Required(ErrorMessage = "Името на графика е задължително.")]
        [StringLength(100, ErrorMessage = "Името на графика трябва да бъде до 100 символа.")]
        public string ScheduleName { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public List<Project> Projects { get; set; }

        // Други полета, специфични за графика

        public Schedule()
        {
            Projects = new List<Project>();
        }

        public Schedule(string scheduleName, DateTime startDate, DateTime endDate)
        {
            ScheduleName = scheduleName;
            StartDate = startDate;
            EndDate = endDate;
            Projects = new List<Project>();

        }
    }
}
