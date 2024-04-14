using Svetlina.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Svetlina.Data.Models
{
	public class Cart
	{
		[Key]
		public int Id { get; set; }
		public List<Product> Products { get; set; }
		public Cart()
		{
			Products = new List<Product>();
		}
	}
	

}
