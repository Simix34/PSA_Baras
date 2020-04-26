

using System.ComponentModel.DataAnnotations.Schema;
/**
* @(#) Order.cs
*/
namespace PSA_Baras.Models
{
	public class Order
	{
		public int Id { get; set; }
		public string delivery_address { get; set; }
		
		public string payment_purpose { get; set; }

		//[ForeignKey("BankAccount")]
		//public int bankAccountId { get; set; }
		//public BankAccount bankAccount { get; set; }

		[ForeignKey("Cart")]
		public int cartId { get; set; }
		public Cart cart { get; set; }

	}
	
}
