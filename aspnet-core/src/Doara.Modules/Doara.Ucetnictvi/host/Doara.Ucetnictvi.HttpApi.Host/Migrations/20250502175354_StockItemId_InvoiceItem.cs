using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Ucetnictvi.Migrations
{
    /// <inheritdoc />
    public partial class StockItemId_InvoiceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StockMovementId",
                table: "Ucetnictvi_InvoiceItem",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockMovementId",
                table: "Ucetnictvi_InvoiceItem");
        }
    }
}
