using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Svetlina.Data.Models
{
    public class Worker
    {
        [Key]
        public int WorkerId { get; set; }

        [Required(ErrorMessage = "Името на работника е задължително.")]
        [StringLength(100, ErrorMessage = "Името на работника трябва да бъде до 100 символа.")]
        public string WorkerName { get; set; }


        [Required(ErrorMessage = "Телефонният номер на работника е задължителен.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Невалиден телефонен номер.")]
        public string PhoneNumber { get; set; }

        //[ForeignKey("ProjectId")]
        //public Project Project { get; set; }

        //public int ProjectId { get; set; }

        //public SpecialisationType SpecialisationType { get; set; }
        public Worker()
        {

        }
        public Worker(int WorkerId, string WorkerName, string PhoneNumber)
        {
            this.WorkerId = WorkerId;
            this.WorkerName = WorkerName;
            this.PhoneNumber = PhoneNumber;
            //SpecialisationType = SpecialisationType;
            //this.Project = Project;
            //ProjectId = Project.ProjectId;

        }
    }
}
