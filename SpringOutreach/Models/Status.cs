using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpringOutreach.Models
{
	public class Status
	{
		[Key]
        public int Id { get; set; }

		public string? Name { get; set; }

        public ICollection<Outreach>? Outreach { get; set; }
    }
}