using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpringOutreach.Models
{
	public class Event
	{
		[Key]
        public int? Id { get; set; }

		public string? Title { get; set; }

		public string? Note { get; set; }

		[DataType(DataType.Date)]
        public DateTime? Date { get; set; }

		public int? OutreachId { get; set; }

		public Outreach? Outreach { get; set; }

		public string? Contact { get; set; }

		public string? StringId { get; set; }

        public string? Time { get; set; }

        [Display(Name = "Input Required")]
        public string? IsInputRequired { get; set; }
	}
}

