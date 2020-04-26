using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSA_Baras.Models
{
	public class User
	{
		public int Id { get; set; }

		public string login { get; set; }

		public string password { get; set; }

		public string email { get; set; }

		public string first_name { get; set; }

		public string last_name { get; set; }

		public int role { get; set; }

		public ICollection<Comment> comments { get; set; }

		public ICollection<Cart> carts { get; set; }

		public ICollection<BankAccount> bank_accounts { get; set; }

	}
}