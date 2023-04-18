using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scavdue.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPlaceFieldToAdminUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "AdministrativeUnits",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "CenterLong",
                table: "AdministrativeUnitPolygons",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "CenterLat",
                table: "AdministrativeUnitPolygons",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place",
                table: "AdministrativeUnits");

            migrationBuilder.AlterColumn<float>(
                name: "CenterLong",
                table: "AdministrativeUnitPolygons",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "CenterLat",
                table: "AdministrativeUnitPolygons",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
