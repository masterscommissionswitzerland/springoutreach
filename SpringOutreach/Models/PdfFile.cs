using System;
namespace SpringOutreach.Models
{
	public class PdfFile
	{
		public int Id { get; set; }

		public byte[]? FileBytes { get; set; }

		public string? FileName { get; set; }

		public int? OutreachId { get; set; }

		public Outreach? Outreach { get; set; }
	}
}

