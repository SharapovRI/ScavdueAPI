using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Scavdue.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTablesForLifeIndexAssessment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationCriteriaTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCriteriaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LifeIndexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceivingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AdministrativeUnitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeIndexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeIndexes_AdministrativeUnits_AdministrativeUnitId",
                        column: x => x.AdministrativeUnitId,
                        principalTable: "AdministrativeUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationCriterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EvaluationCriteriaTypeId = table.Column<int>(type: "integer", nullable: false),
                    LifeIndexId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationCriterias_EvaluationCriteriaTypes_EvaluationCrite~",
                        column: x => x.EvaluationCriteriaTypeId,
                        principalTable: "EvaluationCriteriaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationCriterias_LifeIndexes_LifeIndexId",
                        column: x => x.LifeIndexId,
                        principalTable: "LifeIndexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriterias_EvaluationCriteriaTypeId",
                table: "EvaluationCriterias",
                column: "EvaluationCriteriaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriterias_LifeIndexId",
                table: "EvaluationCriterias",
                column: "LifeIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeIndexes_AdministrativeUnitId",
                table: "LifeIndexes",
                column: "AdministrativeUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationCriterias");

            migrationBuilder.DropTable(
                name: "EvaluationCriteriaTypes");

            migrationBuilder.DropTable(
                name: "LifeIndexes");
        }
    }
}
