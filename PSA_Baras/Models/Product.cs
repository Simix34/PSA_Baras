

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

		public string color { get; set; }

		public string description { get; set; }

		[ForeignKey("Cocktail")]
		public int cocktailId { get; set; }
		public Cocktail cocktail { get; set; }

	}
	
}
