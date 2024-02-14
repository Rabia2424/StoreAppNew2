using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
	public class Category
	{
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        // Collection navigation property
        public ICollection<Product> Products { get; set;}
    }
}
