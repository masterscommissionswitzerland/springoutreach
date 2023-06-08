using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SpringOutreach.Models
{
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Institution Name")]
        public string? Name { get; set; }

        public string? City { get; set; }

        public string? Canton { get; set; }

        public string? Adress { get; set; }

        public string? PlaceNote { get; set; }

        public string? PlaceConnection { get; set; }

        public int? PlaceTypeId { get; set; }

        public PlaceType? PlaceType { get; set; }

        public Contact? Contact { get; set; }

        public ICollection<SecondaryContact>? SecondaryContacts { get; set; }

        [Display(Name = "Last Outreach")]
        public ICollection<Outreach>? Outreaches { get; set; }

        [Display(Name = "Links")]
        public ICollection<PlaceLink>? PlaceLinks { get; set; }
    }
}

