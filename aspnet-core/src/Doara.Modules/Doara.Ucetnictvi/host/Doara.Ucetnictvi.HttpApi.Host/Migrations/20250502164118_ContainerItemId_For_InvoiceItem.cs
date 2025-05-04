using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Ucetnictvi.Migrations
{
    /// <inheritdoc />
    public partial class ContainerItemId_For_InvoiceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VatRate",
                table: "Ucetnictvi_InvoiceItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<Guid>(
                name: "ContainerItemId",
                table: "Ucetnictvi_InvoiceItem",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerItemId",
                table: "Ucetnictvi_InvoiceItem");

            migrationBuilder.AlterColumn<decimal>(
                name: "VatRate",
                table: "Ucetnictvi_InvoiceItem",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
