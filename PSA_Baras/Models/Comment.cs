using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSA_Baras.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string text { get; set; }

		[ForeignKey("User")]
		public int authorId { get; set; }
		public User author { get; set; }

		[ForeignKey("Cocktail")]
		public int cocktailId { get; set; }
		public Cocktail cocktail { get; set; }
	}
}
