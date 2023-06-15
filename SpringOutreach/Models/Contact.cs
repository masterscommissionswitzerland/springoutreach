using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpringOutreach.Models
{
	public class Contact
	{
		[Key]
        public int Id { get; set; }

        [Display(Name = "Contact")]
        public string? Name { get; set; }

		public string? Mail { get; set; }

		public string? Phone { get; set; }

		[Display(Name = "Responsible For")]
		public string? ResponsibleFor { get; set; }

		public int? PositionId { get; set; }

		public Position? Position { get; set; }

		public int PlaceId { get; set; }

		public Place? Place { get; set; }
    }
}

