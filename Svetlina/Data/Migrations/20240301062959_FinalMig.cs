using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Svetlina.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Projects_ProjectId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "SpecialisationType",
                table: "Workers");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Workers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Projects_ProjectId",
                table: "Workers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Projects_ProjectId",
                table: "Workers");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialisationType",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Projects_ProjectId",
                table: "Workers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
