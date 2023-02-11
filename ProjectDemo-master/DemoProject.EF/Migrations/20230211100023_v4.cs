using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoProject.EF.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRequestedToCancel",
                table: "Vacancies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequestedToCancel",
                table: "Vacancies");
        }
    }
}
