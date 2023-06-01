using System;
using System.ComponentModel.DataAnnotations;

namespace SpringOutreach.Models
{
	public class PlaceType
	{
		[Key]
		public int Id { get; set; }

		public string? Title { get; set; }

		public ICollection<Place>? Place { get; set; }
	}
}