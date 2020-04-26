

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
/**
* @(#) Cart_item.cs
*/
namespace PSA_Baras.Models
{
	public class CartItem
	{
		public int Id { get; set; }		

		public int count { get; set; }

		public double price { get; set; }

		[ForeignKey("Cart")]
		public int cartId { get; set; }
		public Cart cart { get; set; }

		public ICollection<Cocktail> cocktails { get; set; }
	}
	
}
