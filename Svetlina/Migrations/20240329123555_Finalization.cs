using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Svetlina.Migrations
{
    /// <inheritdoc />
    public partial class Finalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecialisationType",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialisationType",
                table: "Workers");
        }
    }
}
