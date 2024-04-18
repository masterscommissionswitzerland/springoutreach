using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpringOutreach.CustomAttribute;

namespace SpringOutreach.Models
{
	public class Outreach
	{
		[Key]
        public int Id { get; set; }

		[Display(Name = "Main Contact")]
		public string? InternContact { get; set; }

		[Display(Name = "Responsible Intern")]
		public string? InternResponsible { get; set; }

		public string? StringId { get; set; }

		[Display(Name = "Year")]
		public int? Year { get; set; }

        [Display(Name = "Notes")]
		public string? Note { get; set; }

        [Display(Name = "Last Contact")]
        public string? LastContact { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

		public int? PlaceId { get; set; }

		public Place? Place { get; set; }

		public ICollection<PdfFile>? PdfFile { get; set; }

		public ICollection<Event>? Events { get; set; }

        public int? StatusId { get; set; }

		[Display(Name = "Status")]
        public Status? Status { get; set; }
    }
}

