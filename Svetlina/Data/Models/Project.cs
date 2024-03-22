using System.ComponentModel.DataAnnotations;

namespace Svetlina.Data.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Името на проекта е задължително.")]
        [StringLength(100, ErrorMessage = "Името на проекта трябва да бъде до 100 символа.")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Описание на проекта е задължително.")]
        [StringLength(500, ErrorMessage = "Описанието на проекта трябва да бъде до 500 символа.")]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Worker> Workers { get; set; }

        public Schedule Schedule { get; set; }
        public List<Product> ProjectsProducts { get; set; }

        public Customer Customer { get; set; }





        private Project()
        {
            Workers = new List<Worker>();
            ProjectsProducts = new List<Product>();
        }
        public Project( Schedule schedule, string ProjectName, Customer customer, string Description, DateTime StartDate, DateTime EndDate)
        {
            this.ProjectName = ProjectName;
            this.Description = Description;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            Workers = new List<Worker>();
            ProjectsProducts = new List<Product>();
            Schedule = schedule;
            Customer = customer;
        }
    }
}
