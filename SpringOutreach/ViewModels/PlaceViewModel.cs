using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpringOutreach.CustomAttribute;
using SpringOutreach.Models;

namespace SpringOutreach.ViewModels
{
	public class PlaceViewModel
	{
        [Key]
        public int? Id { get; set; }

        [Display(Name = "Place")]
        public string? Name { get; set; }

        public string? City { get; set; }

        public string? Canton { get; set; }

        public string? Adress { get; set; }

        public string? Note { get; set; }

        [Display(Name = "Kind")]
        public string? SetPlaceType { get; set; }

        public int? PlaceTypeId { get; set; }

        public List<PlaceType>? PlaceTypes { get; set; }

        public Contact? Contact { get; set; }

        [Display(Name = "Name")]
        public string? ContactName { get; set; }

        [Display(Name = "Mail")]
        public string? ContactMail { get; set; }

        [Display(Name = "Phone")]
        public string? ContactPhone { get; set; }

        [Display(Name = "Responsible For")]
        public string? ResponsibleFor { get; set; }

        public ICollection<PlaceLink>? PlaceLinks { get; set; }

        public ICollection<Outreach>? Outreaches { get; set; } = new List<Outreach>();

        public ICollection<Event>? Events { get; set; }

        [Display(Name = "Secondary Contacts")]
        public ICollection<SecondaryContact>? SecondaryContacts { get; set; }

        public int? PositionId { get; set; }

        public List<Position>? Position { get; set; }

        public int? FileId { get; set; }

        public IFormFile? File { get; set; }

        public string? FileName { get; set; }

        [Display(Name = "Notes to this place")]
        public string? PlaceNote { get; set; }

        [Display(Name = "Whats the connection to this place?")]
        public string? PlaceConnection { get; set; }

        public Outreach? CurrentOutreach { get; set; }
    }
}

