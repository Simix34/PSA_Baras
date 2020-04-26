

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
/**
* @(#) Cart.cs
*/
namespace PSA_Baras.Models
{
	public class Cart
	{
		public int Id { get; set; }

		public double full_price { get; set; }

		[ForeignKey("User")]
		public int userId {get;set;}
		public User user { get; set; }

		public ICollection<CartItem> cartItems { get; set; }

		public ICollection<Order> orders { get; set; }		
	}	
}
