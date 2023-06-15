using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpringOutreach.Models
{
	public class PdfFile
	{
		[Key]
        public int Id { get; set; }

		public byte[]? FileBytes { get; set; }

		public string? FileName { get; set; }

		public int? OutreachId { get; set; }

		public Outreach? Outreach { get; set; }
	}
}

