using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Sklady.Migrations
{
    /// <inheritdoc />
    public partial class PresentationPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PresentationPrice",
                table: "Sklady_ContainerItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PresentationPrice",
                table: "Sklady_ContainerItem");
        }
    }
}
