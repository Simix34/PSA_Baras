

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
/**
* @(#) Cocktail.cs
*/
namespace PSA_Baras.Models
{
	public class Cocktail
	{
		public int Id { get; set; }
		public string title { get; set; }

		public double price { get; set; }

		public string color { get; set; }

		public double proof { get; set; }

		public string category { get; set; }

		public ICollection<Comment> comments { get; set; }

		public ICollection<Product> products { get; set; }

		[ForeignKey("CartItem")]
		public int cart_itemId { get; set; }
		public CartItem cart_item { get; set; }

	}
	
}
