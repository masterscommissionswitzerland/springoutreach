using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SpringOutreach.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Canton = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceConnection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Place_Type_PlaceTypeId",
                        column: x => x.PlaceTypeId,
                        principalTable: "Type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: true),
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contact_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Outreach",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternResponsible = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StringId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlaceId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outreach", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Outreach_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Outreach_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlaceLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceLink_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SecondaryContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryContactPositionId = table.Column<int>(type: "int", nullable: true),
                    StringId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondaryContact_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecondaryContact_Position_SecondaryContactPositionId",
                        column: x => x.SecondaryContactPositionId,
                        principalTable: "Position",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OutreachId = table.Column<int>(type: "int", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StringId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInputRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Outreach_OutreachId",
                        column: x => x.OutreachId,
                        principalTable: "Outreach",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PdfFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutreachId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdfFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PdfFile_Outreach_OutreachId",
                        column: x => x.OutreachId,
                        principalTable: "Outreach",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Jugendarbeiter/inn" },
                    { 2, "Pfarrer/inn" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "Asked" },
                    { 3, "Accepted" }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Ref" },
                    { 2, "Kath" }
                });

            migrationBuilder.InsertData(
                table: "Place",
                columns: new[] { "Id", "Adress", "Canton", "City", "Name", "PlaceConnection", "PlaceNote", "PlaceTypeId" },
                values: new object[,]
                {
                    { 1, "Burgfeldweg 13", "Bern", "Thun", "Jahu Thun", null, null, 1 },
                    { 2, "Klosterweg 12", "Zug", "Zug", "Oasis Zug", null, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Mail", "Name", "Phone", "PlaceId", "PositionId", "ResponsibleFor" },
                values: new object[,]
                {
                    { 1, "christina.wenger@jahu.info", "Christina Wenger", "078 941 66 02", 1, 2, null },
                    { 2, "magdalene.soundso@oasis.ch", "Magdalene Soundso", "079 777 02 32", 2, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Outreach",
                columns: new[] { "Id", "EndDate", "InternContact", "InternResponsible", "Note", "PlaceId", "StartDate", "StatusId", "StringId", "Year" },
                values: new object[,]
                {
                    { 1, null, "Nicola Beck", "Eleosa Zürcher", "Wir haben das und das gemacht.", 1, null, 1, "stringId", 2021 },
                    { 2, null, "Micha Wenger", "Elina Josi", "Diese Jahr haben wir etwas anders gemacht.", 1, null, 2, "stringId1", 2022 },
                    { 3, null, "Janis Volz", "Sophia Krebs", "Hier haben wir das getan.", 2, null, 2, "stringId2", 2021 },
                    { 4, null, "Simon Schmidt", "Marlene Roth", "Und im nächsten Jahr das", 2, null, 3, "stringId3", 2022 }
                });

            migrationBuilder.InsertData(
                table: "SecondaryContact",
                columns: new[] { "Id", "Mail", "Name", "Phone", "PlaceId", "ResponsibleFor", "SecondaryContactPositionId", "StringId" },
                values: new object[] { 1, "micha.wenger@jahu.info", "Micha Wenger", "078 324 23 02", 1, null, 1, "stringId4" });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "Contact", "Date", "IsInputRequired", "Note", "OutreachId", "StringId", "Time", "Title" },
                values: new object[] { 1, "Joëlle Wenger", null, false, "Jugendgruppenabend", 1, "stringId5", null, "LightFactory" });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_PlaceId",
                table: "Contact",
                column: "PlaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_PositionId",
                table: "Contact",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_OutreachId",
                table: "Event",
                column: "OutreachId");

            migrationBuilder.CreateIndex(
                name: "IX_Outreach_PlaceId",
                table: "Outreach",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Outreach_StatusId",
                table: "Outreach",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PdfFile_OutreachId",
                table: "PdfFile",
                column: "OutreachId");

            migrationBuilder.CreateIndex(
                name: "IX_Place_PlaceTypeId",
                table: "Place",
                column: "PlaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceLink_PlaceId",
                table: "PlaceLink",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryContact_PlaceId",
                table: "SecondaryContact",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryContact_SecondaryContactPositionId",
                table: "SecondaryContact",
                column: "SecondaryContactPositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "PdfFile");

            migrationBuilder.DropTable(
                name: "PlaceLink");

            migrationBuilder.DropTable(
                name: "SecondaryContact");

            migrationBuilder.DropTable(
                name: "Outreach");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
