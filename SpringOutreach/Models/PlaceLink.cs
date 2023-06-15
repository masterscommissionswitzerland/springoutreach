using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpringOutreach.Models
{
	public class PlaceLink
	{
		[Key]
        public int? Id { get; set; }

		public string? Link { get; set; }

        public int? PlaceId { get; set; }

        public Place? Place { get; set; }
    }
}

