

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

		public ICollection<CocktailProduct> cocktailProducts { get; set; }

		public ICollection<CartItem> CartItems { get; set; }
	}
	
}
