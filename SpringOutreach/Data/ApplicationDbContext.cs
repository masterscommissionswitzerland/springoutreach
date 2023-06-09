using System.Reflection.Emit;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SpringOutreach.Models;

namespace SpringOutreach.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SpringOutreach.Models.Place> Place { get; set; }
    public virtual DbSet<SpringOutreach.Models.Contact> Contact { get; set; }
    public virtual DbSet<SpringOutreach.Models.Position> Position { get; set; }
    public virtual DbSet<SpringOutreach.Models.PlaceType> Type { get; set; }
    public virtual DbSet<SpringOutreach.Models.Outreach> Outreach { get; set; }
    public virtual DbSet<SpringOutreach.Models.PdfFile> PdfFile { get; set; }
    public virtual DbSet<SpringOutreach.Models.SecondaryContact> SecondaryContact { get; set; }
    public virtual DbSet<SpringOutreach.Models.PlaceLink> PlaceLink { get; set; }
    public virtual DbSet<SpringOutreach.Models.Event> Event { get; set; }
    public virtual DbSet<SpringOutreach.Models.Status> Status { get; set; }

    #region Required
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Place>().HasOne(x => x.Contact).WithOne(x => x.Place).HasForeignKey<Contact>(x => x.PlaceId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Place>().HasMany(x => x.SecondaryContacts).WithOne(x => x.Place).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Place>().HasMany(x => x.PlaceLinks).WithOne(x => x.Place).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Place>().HasOne(x => x.PlaceType).WithMany(x => x.Place).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Place>().HasMany(x => x.Outreaches).WithOne(x => x.Place).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Outreach>().HasMany(x => x.Events).WithOne(x => x.Outreach).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Outreach>().HasOne(x => x.Status).WithMany(x => x.Outreach).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Outreach>().HasMany(x => x.PdfFile).WithOne(x => x.Outreach).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Place>().HasData(
            new Place { Id = 1, Name = "Jahu Thun", City = "Thun", Canton = "Bern", Adress = "Burgfeldweg 13", PlaceTypeId = 1 },
            new Place { Id = 2, Name = "Oasis Zug", City = "Zug", Canton = "Zug", Adress = "Klosterweg 12", PlaceTypeId = 2 }
            );

        builder.Entity<Models.PlaceType>().HasData(
            new Models.PlaceType { Id = 1, Title = "Ref" },
            new Models.PlaceType { Id = 2, Title = "Kath" }
            );

        builder.Entity<Models.Contact>().HasData(
            new Contact { Id = 1, Name = "Christina Wenger", Mail = "christina.wenger@jahu.info", Phone = "078 941 66 02", PositionId = 2, PlaceId = 1 },
            new Contact { Id = 2, Name = "Magdalene Soundso", Mail = "magdalene.soundso@oasis.ch", Phone = "079 777 02 32", PositionId = 1, PlaceId = 2 }
            );

        builder.Entity<Models.SecondaryContact>().HasData(
            new SecondaryContact { Id = 1, Name = "Micha Wenger", Mail = "micha.wenger@jahu.info", Phone = "078 324 23 02", SecondaryContactPositionId = 1, PlaceId = 1, StringId = "stringId4" }
            );

        builder.Entity<Models.Position>().HasData(
            new Position { Id = 1, Title = "Jugendarbeiter/inn" },
            new Position { Id = 2, Title = "Pfarrer/inn" }
            );

        builder.Entity<Models.Outreach>().HasData(
            new Outreach { Id = 1, InternContact = "Nicola Beck", InternResponsible = "Eleosa Zürcher", Year = 2021, Note = "Wir haben das und das gemacht.", StringId = "stringId", PlaceId = 1, StatusId = 1 },
            new Outreach { Id = 2, InternContact = "Micha Wenger", InternResponsible = "Elina Josi", Year = 2022, Note = "Diese Jahr haben wir etwas anders gemacht.", StringId = "stringId1", PlaceId = 1, StatusId = 2 },
            new Outreach { Id = 3, InternContact = "Janis Volz", InternResponsible = "Sophia Krebs", Year = 2021, Note = "Hier haben wir das getan.", StringId = "stringId2", PlaceId = 2, StatusId = 2 },
            new Outreach { Id = 4, InternContact = "Simon Schmidt", InternResponsible = "Marlene Roth", Year = 2022, Note = "Und im nächsten Jahr das", StringId = "stringId3", PlaceId = 2, StatusId = 3 }
            );

        builder.Entity<Models.Status>().HasData(
            new Status { Id = 1, Name = "Open" },
            new Status { Id = 2, Name = "Asked" },
            new Status { Id = 3, Name = "Accepted" }
            );

        builder.Entity<Models.Event>().HasData(
            new Event { Id = 1, Title = "LightFactory", Note = "Jugendgruppenabend", OutreachId = 1, Contact = "Joëlle Wenger", StringId = "stringId5" }
            );

        builder.Entity<Models.Place>();
        builder.Entity<Models.PlaceType>();
        builder.Entity<Models.Contact>();
        builder.Entity<Models.Position>();
        builder.Entity<Models.Outreach>();
        builder.Entity<Models.Event>();
        builder.Entity<Models.SecondaryContact>();
        builder.Entity<Models.PdfFile>();
        builder.Entity<Models.Status>();
    }
    #endregion
}