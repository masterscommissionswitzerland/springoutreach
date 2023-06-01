using System;
using System.ComponentModel.DataAnnotations;

namespace SpringOutreach.Models
{
	public class Position
	{
		public int Id { get; set; }

		public string? Title { get; set; }

		public ICollection<Contact>? Contact { get; set; }

		public ICollection<SecondaryContact>? SecondaryContact { get; set; }
	}
}

