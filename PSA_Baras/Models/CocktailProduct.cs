using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSA_Baras.Models
{
    public class CocktailProduct
	{
		public int Id { get; set; }
		public string unit { get; set; }
		public double quantity { get; set; }

		[ForeignKey("Cocktail")]
		public int cocktailId { get; set; }
		public Cocktail cocktail { get; set; }

		[ForeignKey("Product")]
		public int productId { get; set; }
		public Product product { get; set; }
	}
}
