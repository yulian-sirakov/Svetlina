using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Svetlina.Data.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required(ErrorMessage = "Името на отчета е задължително.")]
        [StringLength(100, ErrorMessage = "Името на отчета трябва да бъде до 100 символа.")]
        public string ReportName { get; set; }

        [Required(ErrorMessage = "Съдържанието на отчета е задължително.")]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public string CustomerId { get; set; }



        public Report()
        {
            // Празен конструктор
        }

        public Report(string reportName, string content, DateTime dateCreated, Customer customer)
        {
            ReportName = reportName;
            Content = content;
            DateCreated = dateCreated;
            Customer = customer;
        }
    }
}
