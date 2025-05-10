using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceType",
                table: "Ucetnictvi_Invoice",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "Ucetnictvi_Invoice");
        }
    }
}
