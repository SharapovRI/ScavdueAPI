using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scavdue.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdministrativeUnit_AddLoopRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdministrativeLevel",
                table: "AdministrativeUnits",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentAdministrativeUnitId",
                table: "AdministrativeUnits",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativeUnits_ParentAdministrativeUnitId",
                table: "AdministrativeUnits",
                column: "ParentAdministrativeUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdministrativeUnits_AdministrativeUnits_ParentAdministrativ~",
                table: "AdministrativeUnits",
                column: "ParentAdministrativeUnitId",
                principalTable: "AdministrativeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdministrativeUnits_AdministrativeUnits_ParentAdministrativ~",
                table: "AdministrativeUnits");

            migrationBuilder.DropIndex(
                name: "IX_AdministrativeUnits_ParentAdministrativeUnitId",
                table: "AdministrativeUnits");

            migrationBuilder.DropColumn(
                name: "AdministrativeLevel",
                table: "AdministrativeUnits");

            migrationBuilder.DropColumn(
                name: "ParentAdministrativeUnitId",
                table: "AdministrativeUnits");
        }
    }
}
