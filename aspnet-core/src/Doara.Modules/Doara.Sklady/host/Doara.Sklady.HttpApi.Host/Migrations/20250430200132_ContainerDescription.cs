using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Sklady.Migrations
{
    /// <inheritdoc />
    public partial class ContainerDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sklady_Container",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sklady_Container");
        }
    }
}
