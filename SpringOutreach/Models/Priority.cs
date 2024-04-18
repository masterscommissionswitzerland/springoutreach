using System;
using System.ComponentModel.DataAnnotations;

namespace SpringOutreach.Models
{
	public class Priority
	{
        [Key]
        public int Id { get; set; }

        public string? Level { get; set; }

        public ICollection<Place>? Place { get; set; }
  	}
}

