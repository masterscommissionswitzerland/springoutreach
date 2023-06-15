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
    public virtual DbSet<SpringOutreach.Models.PlaceType> PlaceTypes { get; set; }

    #region Required
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Place>().HasOne(x => x.Contact).WithOne(x => x.Place)
            .HasForeignKey<Contact>(x => x.PlaceId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<SecondaryContact>().HasOne(x => x.Place).WithMany(x => x.SecondaryContacts)
            .HasForeignKey(x => x.PlaceId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<PlaceLink>().HasOne(x => x.Place).WithMany(x => x.PlaceLinks)
            .HasForeignKey(x => x.PlaceId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Outreach>().HasOne(x => x.Place).WithMany(x => x.Outreaches)
            .HasForeignKey(x => x.PlaceId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Place>().HasOne(x => x.PlaceType).WithMany(x => x.Place)
            .HasForeignKey(x => x.PlaceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Outreach>().HasMany(x => x.Events).WithOne(x => x.Outreach)
            .HasForeignKey(x => x.OutreachId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Outreach>().HasMany(x => x.PdfFile).WithOne(x => x.Outreach)
            .HasForeignKey(x => x.OutreachId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Outreach>().HasOne(x => x.Status).WithMany(x => x.Outreach)
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Models.Place>();
        builder.Entity<Models.PlaceType>();
        builder.Entity<Models.Contact>();
        builder.Entity<Models.Position>();
        builder.Entity<Models.Outreach>();
        builder.Entity<Models.Event>();
        builder.Entity<Models.SecondaryContact>();
        builder.Entity<Models.Status>();
    }
    #endregion
}
