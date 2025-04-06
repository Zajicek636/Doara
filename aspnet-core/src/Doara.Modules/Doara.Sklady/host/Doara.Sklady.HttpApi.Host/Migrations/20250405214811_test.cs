using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Sklady.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PresentationPrice",
                table: "Sklady_ContainerItem");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Sklady_WarehouseWorker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Sklady_WarehouseWorker",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Sklady_WarehouseWorker",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "Sklady_WarehouseWorker",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Sklady_WarehouseWorker");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Sklady_WarehouseWorker");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Sklady_WarehouseWorker");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Sklady_WarehouseWorker");

            migrationBuilder.AddColumn<decimal>(
                name: "PresentationPrice",
                table: "Sklady_ContainerItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
