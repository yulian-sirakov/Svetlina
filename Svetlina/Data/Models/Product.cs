using System.ComponentModel.DataAnnotations;

namespace Svetlina.Data.Models
{
    public class Product
    {

        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Името на продукта е задължително.")]
        [StringLength(100, ErrorMessage = "Името на продукта трябва да бъде до 100 символа.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Цената на продукта е задължителна.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цената на продукта трябва да бъде положително число.")]
        public decimal Price { get; set; }
        public string ProductImage {  get; set; }
        public List<Project> ProjectsProducts { get; set; }
        public Product()
        {
            ProjectsProducts = new List<Project>();
        }

        public Product(int ProductId, string productName, decimal price)
        {
            this.ProductId = ProductId;
            this.ProductName = productName;
            this.Price = price;
            ProjectsProducts = new List<Project>();


        }
    }
}
