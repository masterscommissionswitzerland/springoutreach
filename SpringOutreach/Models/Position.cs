using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpringOutreach.Models
{
	public class Position
	{
		[Key]
        public int Id { get; set; }

		public string? Title { get; set; }

		public ICollection<Contact>? Contact { get; set; }

		public ICollection<SecondaryContact>? SecondaryContact { get; set; }
	}
}

