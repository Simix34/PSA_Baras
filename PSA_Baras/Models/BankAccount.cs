

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
/**
* @(#) Bank account.cs
*/
namespace PSA_Baras.Models
{
	public class BankAccount
	{
		public int Id { get; set; }
		public string account_number { get; set; }

		public string bank { get; set; }

		public int card_number { get; set; }

		public DateTime card_validity { get; set; }

		[ForeignKey("User")]
		public int ownerId { get; set; }
		public User owner { get; set; }
		//public ICollection<Order> orders { get; set; }

	}
	
}
