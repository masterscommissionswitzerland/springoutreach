CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "PlaceType" (
    "Id" SERIAL PRIMARY KEY,
    "Title" TEXT NULL
	);

CREATE TABLE "Position" (
    "Id" SERIAL PRIMARY KEY,
    "Title" TEXT NULL
);

CREATE TABLE "Status" (
    "Id" SERIAL PRIMARY KEY,
    "Name" TEXT NULL
);

CREATE TABLE "Place" (
    "Id" SERIAL PRIMARY KEY,
    "Name" TEXT NULL,
    "City" TEXT NULL,
    "Canton" TEXT NULL,
    "Adress" TEXT NULL,
    "PlaceNote" TEXT NULL,
    "PlaceConnection" TEXT NULL,
    "PlaceTypeId" INTEGER NULL,
    CONSTRAINT "FK_Place_PlaceType_PlaceTypeId" FOREIGN KEY ("PlaceTypeId") REFERENCES "PlaceType" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Contact" (
    "Id" SERIAL PRIMARY KEY,
    "Name" TEXT NULL,
    "Mail" TEXT NULL,
    "Phone" TEXT NULL,
    "ResponsibleFor" TEXT NULL,
    "PositionId" INTEGER NULL,
    "PlaceId" INTEGER NOT NULL,
    CONSTRAINT "FK_Contact_Place_PlaceId" FOREIGN KEY ("PlaceId") REFERENCES "Place" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Contact_Position_PositionId" FOREIGN KEY ("PositionId") REFERENCES "Position" ("Id")
);

CREATE TABLE "Outreach" (
    "Id" SERIAL PRIMARY KEY,
    "InternContact" TEXT NULL,
    "InternResponsible" TEXT NULL,
    "StringId" TEXT NULL,
    "Year" INTEGER NULL,
    "Note" TEXT NULL,
    "StartDate" TEXT NULL,
    "EndDate" TEXT NULL,
    "PlaceId" INTEGER NULL,
    "StatusId" INTEGER NULL,
    CONSTRAINT "FK_Outreach_Place_PlaceId" FOREIGN KEY ("PlaceId") REFERENCES "Place" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Outreach_Status_StatusId" FOREIGN KEY ("StatusId") REFERENCES "Status" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "PlaceLink" (
    "Id" SERIAL PRIMARY KEY,
    "Link" TEXT NULL,
    "PlaceId" INTEGER NULL,
    CONSTRAINT "FK_PlaceLink_Place_PlaceId" FOREIGN KEY ("PlaceId") REFERENCES "Place" ("Id") ON DELETE CASCADE
);

CREATE TABLE "SecondaryContact" (
    "Id" SERIAL PRIMARY KEY,
    "Name" TEXT NULL,
    "Mail" TEXT NULL,
    "Phone" TEXT NULL,
    "ResponsibleFor" TEXT NULL,
    "SecondaryContactPositionId" INTEGER NULL,
    "StringId" TEXT NULL,
    "PlaceId" INTEGER NOT NULL,
    CONSTRAINT "FK_SecondaryContact_Place_PlaceId" FOREIGN KEY ("PlaceId") REFERENCES "Place" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_SecondaryContact_Position_SecondaryContactPositionId" FOREIGN KEY ("SecondaryContactPositionId") REFERENCES "Position" ("Id")
);

CREATE TABLE "Event" (
    "Id" SERIAL PRIMARY KEY,
    "Title" TEXT NULL,
    "Note" TEXT NULL,
    "Date" TEXT NULL,
    "OutreachId" INTEGER NULL,
    "Contact" TEXT NULL,
    "StringId" TEXT NULL,
    "Time" TEXT NULL,
    "IsInputRequired" TEXT NULL,
    CONSTRAINT "FK_Event_Outreach_OutreachId" FOREIGN KEY ("OutreachId") REFERENCES "Outreach" ("Id") ON DELETE CASCADE
);

CREATE TABLE "PdfFile" (
    "Id" SERIAL PRIMARY KEY,
    "FileBytes" BYTEA NULL,
    "FileName" TEXT NULL,
    "OutreachId" INTEGER NULL,
    CONSTRAINT "FK_PdfFile_Outreach_OutreachId" FOREIGN KEY ("OutreachId") REFERENCES "Outreach" ("Id") ON DELETE CASCADE
);

INSERT INTO "PlaceType" ("Id", "Title")
VALUES (1, 'Ref');
INSERT INTO "PlaceType" ("Id", "Title")
VALUES (2, 'Kath');

INSERT INTO "Position" ("Id", "Title")
VALUES (1, 'Jugendarbeiter/inn');
INSERT INTO "Position" ("Id", "Title")
VALUES (2, 'Pfarrer/inn');

INSERT INTO "Status" ("Id", "Name")
VALUES (1, 'Open');
INSERT INTO "Status" ("Id", "Name")
VALUES (2, 'Asked');
INSERT INTO "Status" ("Id", "Name")
VALUES (3, 'Accepted');

INSERT INTO "Place" ("Id", "Adress", "Canton", "City", "Name", "PlaceConnection", "PlaceNote", "PlaceTypeId")
VALUES (1, 'Burgfeldweg 13', 'Bern', 'Thun', 'Jahu Thun', NULL, NULL, 1);
INSERT INTO "Place" ("Id", "Adress", "Canton", "City", "Name", "PlaceConnection", "PlaceNote", "PlaceTypeId")
VALUES (2, 'Klosterweg 12', 'Zug', 'Zug', 'Oasis Zug', NULL, NULL, 2);

INSERT INTO "Contact" ("Id", "Mail", "Name", "Phone", "PlaceId", "PositionId", "ResponsibleFor")
VALUES (1, 'christina.wenger@jahu.info', 'Christina Wenger', '078 941 66 02', 1, 2, NULL);
INSERT INTO "Contact" ("Id", "Mail", "Name", "Phone", "PlaceId", "PositionId", "ResponsibleFor")
VALUES (2, 'magdalene.soundso@oasis.ch', 'Magdalene Soundso', '079 777 02 32', 2, 1, NULL);

INSERT INTO "Outreach" ("Id", "EndDate", "InternContact", "InternResponsible", "Note", "PlaceId", "StartDate", "StatusId", "StringId", "Year")
VALUES (1, NULL, 'Nicola Beck', 'Eleosa Zürcher', 'Wir haben das und das gemacht.', 1, NULL, 1, 'stringId', 2021);
INSERT INTO "Outreach" ("Id", "EndDate", "InternContact", "InternResponsible", "Note", "PlaceId", "StartDate", "StatusId", "StringId", "Year")
VALUES (2, NULL, 'Micha Wenger', 'Elina Josi', 'Diese Jahr haben wir etwas anders gemacht.', 1, NULL, 2, 'stringId1', 2022);
INSERT INTO "Outreach" ("Id", "EndDate", "InternContact", "InternResponsible", "Note", "PlaceId", "StartDate", "StatusId", "StringId", "Year")
VALUES (3, NULL, 'Janis Volz', 'Sophia Krebs', 'Hier haben wir das getan.', 2, NULL, 2, 'stringId2', 2021);
INSERT INTO "Outreach" ("Id", "EndDate", "InternContact", "InternResponsible", "Note", "PlaceId", "StartDate", "StatusId", "StringId", "Year")
VALUES (4, NULL, 'Simon Schmidt', 'Marlene Roth', 'Und im nächsten Jahr das', 2, NULL, 3, 'stringId3', 2022);

INSERT INTO "SecondaryContact" ("Id", "Mail", "Name", "Phone", "PlaceId", "ResponsibleFor", "SecondaryContactPositionId", "StringId")
VALUES (1, 'micha.wenger@jahu.info', 'Micha Wenger', '078 324 23 02', 1, NULL, 1, 'stringId4');

INSERT INTO "Event" ("Id", "Contact", "Date", "IsInputRequired", "Note", "OutreachId", "StringId", "Time", "Title")
VALUES (1, 'Joëlle Wenger', NULL, NULL, 'Jugendgruppenabend', 1, 'stringId5', NULL, 'LightFactory');

CREATE UNIQUE INDEX "IX_Contact_PlaceId" ON "Contact" ("PlaceId");

CREATE INDEX "IX_Contact_PositionId" ON "Contact" ("PositionId");

CREATE INDEX "IX_Event_OutreachId" ON "Event" ("OutreachId");

CREATE INDEX "IX_Outreach_PlaceId" ON "Outreach" ("PlaceId");

CREATE INDEX "IX_Outreach_StatusId" ON "Outreach" ("StatusId");

CREATE INDEX "IX_PdfFile_OutreachId" ON "PdfFile" ("OutreachId");

CREATE INDEX "IX_Place_PlaceTypeId" ON "Place" ("PlaceTypeId");

CREATE INDEX "IX_PlaceLink_PlaceId" ON "PlaceLink" ("PlaceId");

CREATE INDEX "IX_SecondaryContact_PlaceId" ON "SecondaryContact" ("PlaceId");

CREATE INDEX "IX_SecondaryContact_SecondaryContactPositionId" ON "SecondaryContact" ("SecondaryContactPositionId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230614072506_init', '7.0.5');

COMMIT;

