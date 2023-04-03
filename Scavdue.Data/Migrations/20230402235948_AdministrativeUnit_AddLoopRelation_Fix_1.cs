using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scavdue.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdministrativeUnit_AddLoopRelation_Fix_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentAdministrativeUnitId",
                table: "AdministrativeUnits",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentAdministrativeUnitId",
                table: "AdministrativeUnits",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
