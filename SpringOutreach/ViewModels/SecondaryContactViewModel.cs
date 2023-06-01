using System;
using System.ComponentModel.DataAnnotations;
using SpringOutreach.Models;

namespace SpringOutreach.ViewModels
{
	public class SecondaryContactViewModel
	{
		[Key]
		public int? Id { get; set; }

		public string? Name { get; set; }

		public string? Mail { get; set; }

		public string? Phone { get; set; }

		[Display(Name = "Responsible For")]
		public string? ResponsibleFor { get; set; }

		[Display(Name = "Secondary Contact")]
		public int? SecondaryContactPositionId { get; set; }

        [Display(Name = "Contact")]
        public List<Position>? SecondaryContactPosition { get; set; }

		public string? StringId { get; set; }

		public int? PlaceId { get; set; }

		public Place? Place { get; set; }
	}
}

