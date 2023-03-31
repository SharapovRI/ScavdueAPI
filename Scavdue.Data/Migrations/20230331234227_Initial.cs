using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Scavdue.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Iso3166 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Country_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitObjectClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UnitObjectClass_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdministrativeUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AdministrativeUnit_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministrativeUnits_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitObjectTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UnitObjectClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UnitObjectType_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitObjectTypes_UnitObjectClasses_UnitObjectClassId",
                        column: x => x.UnitObjectClassId,
                        principalTable: "UnitObjectClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdministrativeUnitPolygons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdministrativeUnitId = table.Column<int>(type: "integer", nullable: false),
                    CenterLong = table.Column<float>(type: "real", nullable: false),
                    CenterLat = table.Column<float>(type: "real", nullable: false),
                    Coordinates = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AdministrativeUnitPolygon_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministrativeUnitPolygons_AdministrativeUnits_Administrati~",
                        column: x => x.AdministrativeUnitId,
                        principalTable: "AdministrativeUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Populations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdministrativeUnitId = table.Column<int>(type: "integer", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Population_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Populations_AdministrativeUnits_AdministrativeUnitId",
                        column: x => x.AdministrativeUnitId,
                        principalTable: "AdministrativeUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AdministrativeUnitId = table.Column<int>(type: "integer", nullable: false),
                    UnitObjectTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UnitObject_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitObjects_AdministrativeUnits_AdministrativeUnitId",
                        column: x => x.AdministrativeUnitId,
                        principalTable: "AdministrativeUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitObjects_UnitObjectTypes_UnitObjectTypeId",
                        column: x => x.UnitObjectTypeId,
                        principalTable: "UnitObjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UnitObjectPolygons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitObjectId = table.Column<int>(type: "integer", nullable: false),
                    CenterLong = table.Column<float>(type: "real", nullable: false),
                    CenterLat = table.Column<float>(type: "real", nullable: false),
                    Coordinates = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UnitObjectPolygon_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitObjectPolygons_UnitObjects_UnitObjectId",
                        column: x => x.UnitObjectId,
                        principalTable: "UnitObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativeUnitPolygons_AdministrativeUnitId",
                table: "AdministrativeUnitPolygons",
                column: "AdministrativeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativeUnits_CountryId",
                table: "AdministrativeUnits",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Populations_AdministrativeUnitId",
                table: "Populations",
                column: "AdministrativeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitObjectPolygons_UnitObjectId",
                table: "UnitObjectPolygons",
                column: "UnitObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitObjects_AdministrativeUnitId",
                table: "UnitObjects",
                column: "AdministrativeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitObjects_UnitObjectTypeId",
                table: "UnitObjects",
                column: "UnitObjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitObjectTypes_UnitObjectClassId",
                table: "UnitObjectTypes",
                column: "UnitObjectClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdministrativeUnitPolygons");

            migrationBuilder.DropTable(
                name: "Populations");

            migrationBuilder.DropTable(
                name: "UnitObjectPolygons");

            migrationBuilder.DropTable(
                name: "UnitObjects");

            migrationBuilder.DropTable(
                name: "AdministrativeUnits");

            migrationBuilder.DropTable(
                name: "UnitObjectTypes");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "UnitObjectClasses");
        }
    }
}
