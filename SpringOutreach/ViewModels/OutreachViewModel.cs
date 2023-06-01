using System;
using System.ComponentModel.DataAnnotations;
using SpringOutreach.CustomAttribute;
using SpringOutreach.Models;

namespace SpringOutreach.ViewModels
{
	public class OutreachViewModel
	{
		[Key]
		public int? Id { get; set; }

		[Display(Name = "Main Contact")]
		public string? InternContact { get; set; }

		[Display(Name = "Responsible Intern")]
		public string? InternResponsible { get; set; }

		public string? StringId { get; set; }

		public int? Year { get; set; }

		[Display(Name = "Notes")]
		public string? Note { get; set; }

		public int? PlaceId { get; set; }

		public Place? Place { get; set; }

        public int? FileId { get; set; }

        public IFormFile? File { get; set; }

        public string? FileName { get; set; }

		public int? OutreachId { get; set; }

		public Outreach? Outreach { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public int? StatusId { get; set; }

		[Display(Name = "Status")]
        public List<Status>? Statuses { get; set; }
    }
}

