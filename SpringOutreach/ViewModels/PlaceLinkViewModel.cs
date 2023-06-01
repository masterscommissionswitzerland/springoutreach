using System;
using SpringOutreach.Models;
using System.ComponentModel.DataAnnotations;

namespace SpringOutreach.ViewModels
{
	public class PlaceLinkViewModel
	{
        [Key]
        public int? Id { get; set; }

        public string? Link { get; set; }

        public int? PlaceId { get; set; }

        public Place? Place { get; set; }
    }
}

