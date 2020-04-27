

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
/**
* @(#) Product.cs
*/
namespace PSA_Baras.Models
{
	public class Product
	{
		public int Id { get; set; }

		public string title { get; set; }

		public string units { get; set; }

		public string color { get; set; }

		public string description { get; set; }

		public ICollection<CocktailProduct> cocktailProducts { get; set; }

	}
	
}
