﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SpringOutreach.Data;

#nullable disable

namespace SpringOutreach.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SpringOutreach.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Mail")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<int?>("PositionId")
                        .HasColumnType("integer");

                    b.Property<string>("ResponsibleFor")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId")
                        .IsUnique();

                    b.HasIndex("PositionId");

                    b.ToTable("Contact");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Mail = "christina.wenger@jahu.info",
                            Name = "Christina Wenger",
                            Phone = "078 941 66 02",
                            PlaceId = 1,
                            PositionId = 2
                        },
                        new
                        {
                            Id = 2,
                            Mail = "magdalene.soundso@oasis.ch",
                            Name = "Magdalene Soundso",
                            Phone = "079 777 02 32",
                            PlaceId = 2,
                            PositionId = 1
                        });
                });

            modelBuilder.Entity("SpringOutreach.Models.Event", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Contact")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IsInputRequired")
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<int?>("OutreachId")
                        .HasColumnType("integer");

                    b.Property<string>("StringId")
                        .HasColumnType("text");

                    b.Property<string>("Time")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OutreachId");

                    b.ToTable("Event");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Contact = "Joëlle Wenger",
                            Note = "Jugendgruppenabend",
                            OutreachId = 1,
                            StringId = "stringId5",
                            Title = "LightFactory"
                        });
                });

            modelBuilder.Entity("SpringOutreach.Models.Outreach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("InternContact")
                        .HasColumnType("text");

                    b.Property<string>("InternResponsible")
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("StringId")
                        .HasColumnType("text");

                    b.Property<int?>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.HasIndex("StatusId");

                    b.ToTable("Outreach");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InternContact = "Nicola Beck",
                            InternResponsible = "Eleosa Zürcher",
                            Note = "Wir haben das und das gemacht.",
                            PlaceId = 1,
                            StatusId = 1,
                            StringId = "stringId",
                            Year = 2021
                        },
                        new
                        {
                            Id = 2,
                            InternContact = "Micha Wenger",
                            InternResponsible = "Elina Josi",
                            Note = "Diese Jahr haben wir etwas anders gemacht.",
                            PlaceId = 1,
                            StatusId = 2,
                            StringId = "stringId1",
                            Year = 2022
                        },
                        new
                        {
                            Id = 3,
                            InternContact = "Janis Volz",
                            InternResponsible = "Sophia Krebs",
                            Note = "Hier haben wir das getan.",
                            PlaceId = 2,
                            StatusId = 2,
                            StringId = "stringId2",
                            Year = 2021
                        },
                        new
                        {
                            Id = 4,
                            InternContact = "Simon Schmidt",
                            InternResponsible = "Marlene Roth",
                            Note = "Und im nächsten Jahr das",
                            PlaceId = 2,
                            StatusId = 3,
                            StringId = "stringId3",
                            Year = 2022
                        });
                });

            modelBuilder.Entity("SpringOutreach.Models.PdfFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("FileBytes")
                        .HasColumnType("bytea");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<int?>("OutreachId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OutreachId");

                    b.ToTable("PdfFile");
                });

            modelBuilder.Entity("SpringOutreach.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .HasColumnType("text");

                    b.Property<string>("Canton")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PlaceConnection")
                        .HasColumnType("text");

                    b.Property<string>("PlaceNote")
                        .HasColumnType("text");

                    b.Property<int?>("PlaceTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlaceTypeId");

                    b.ToTable("Place");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Adress = "Burgfeldweg 13",
                            Canton = "Bern",
                            City = "Thun",
                            Name = "Jahu Thun",
                            PlaceTypeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Adress = "Klosterweg 12",
                            Canton = "Zug",
                            City = "Zug",
                            Name = "Oasis Zug",
                            PlaceTypeId = 2
                        });
                });

            modelBuilder.Entity("SpringOutreach.Models.PlaceLink", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlaceLink");
                });

            modelBuilder.Entity("SpringOutreach.Models.PlaceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PlaceType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Ref"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Kath"
                        });
                });

            modelBuilder.Entity("SpringOutreach.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Position");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Jugendarbeiter/inn"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Pfarrer/inn"
                        });
                });

            modelBuilder.Entity("SpringOutreach.Models.SecondaryContact", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Mail")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<string>("ResponsibleFor")
                        .HasColumnType("text");

                    b.Property<int?>("SecondaryContactPositionId")
                        .HasColumnType("integer");

                    b.Property<string>("StringId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.HasIndex("SecondaryContactPositionId");

                    b.ToTable("SecondaryContact");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Mail = "micha.wenger@jahu.info",
                            Name = "Micha Wenger",
                            Phone = "078 324 23 02",
                            PlaceId = 1,
                            SecondaryContactPositionId = 1,
                            StringId = "stringId4"
                        });
                });

            modelBuilder.Entity("SpringOutreach.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Open"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Asked"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Accepted"
                        });
                });

            modelBuilder.Entity("SpringOutreach.Models.Contact", b =>
                {
                    b.HasOne("SpringOutreach.Models.Place", "Place")
                        .WithOne("Contact")
                        .HasForeignKey("SpringOutreach.Models.Contact", "PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpringOutreach.Models.Position", "Position")
                        .WithMany("Contact")
                        .HasForeignKey("PositionId");

                    b.Navigation("Place");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("SpringOutreach.Models.Event", b =>
                {
                    b.HasOne("SpringOutreach.Models.Outreach", "Outreach")
                        .WithMany("Events")
                        .HasForeignKey("OutreachId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Outreach");
                });

            modelBuilder.Entity("SpringOutreach.Models.Outreach", b =>
                {
                    b.HasOne("SpringOutreach.Models.Place", "Place")
                        .WithMany("Outreaches")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SpringOutreach.Models.Status", "Status")
                        .WithMany("Outreach")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Place");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("SpringOutreach.Models.PdfFile", b =>
                {
                    b.HasOne("SpringOutreach.Models.Outreach", "Outreach")
                        .WithMany("PdfFile")
                        .HasForeignKey("OutreachId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Outreach");
                });

            modelBuilder.Entity("SpringOutreach.Models.Place", b =>
                {
                    b.HasOne("SpringOutreach.Models.PlaceType", "PlaceType")
                        .WithMany("Place")
                        .HasForeignKey("PlaceTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("PlaceType");
                });

            modelBuilder.Entity("SpringOutreach.Models.PlaceLink", b =>
                {
                    b.HasOne("SpringOutreach.Models.Place", "Place")
                        .WithMany("PlaceLinks")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Place");
                });

            modelBuilder.Entity("SpringOutreach.Models.SecondaryContact", b =>
                {
                    b.HasOne("SpringOutreach.Models.Place", "Place")
                        .WithMany("SecondaryContacts")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpringOutreach.Models.Position", "SecondaryContactPosition")
                        .WithMany("SecondaryContact")
                        .HasForeignKey("SecondaryContactPositionId");

                    b.Navigation("Place");

                    b.Navigation("SecondaryContactPosition");
                });

            modelBuilder.Entity("SpringOutreach.Models.Outreach", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("PdfFile");
                });

            modelBuilder.Entity("SpringOutreach.Models.Place", b =>
                {
                    b.Navigation("Contact");

                    b.Navigation("Outreaches");

                    b.Navigation("PlaceLinks");

                    b.Navigation("SecondaryContacts");
                });

            modelBuilder.Entity("SpringOutreach.Models.PlaceType", b =>
                {
                    b.Navigation("Place");
                });

            modelBuilder.Entity("SpringOutreach.Models.Position", b =>
                {
                    b.Navigation("Contact");

                    b.Navigation("SecondaryContact");
                });

            modelBuilder.Entity("SpringOutreach.Models.Status", b =>
                {
                    b.Navigation("Outreach");
                });
#pragma warning restore 612, 618
        }
    }
}
