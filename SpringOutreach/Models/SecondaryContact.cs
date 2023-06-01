using System;
using System.ComponentModel.DataAnnotations;

namespace SpringOutreach.Models
{
	public class SecondaryContact
	{
		[Key]
		public int? Id { get; set; }

        [Display(Name = "Secondary Contact")]
        public string? Name { get; set; }

        public string? Mail { get; set; }

        public string? Phone { get; set; }

        public string? ResponsibleFor { get; set; }

        public int? SecondaryContactPositionId { get; set; }

        public Position? SecondaryContactPosition { get; set; }

        public string? StringId { get; set; }

        public int PlaceId { get; set; }

        public Place? Place { get; set; }
    }
}

