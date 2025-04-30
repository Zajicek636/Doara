using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Sklady.Migrations
{
    /// <inheritdoc />
    public partial class OdebraniSkladnika : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sklady_WarehouseWorker");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sklady_WarehouseWorker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sklady_WarehouseWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sklady_WarehouseWorker_Sklady_Container_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Sklady_Container",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sklady_WarehouseWorker_ContainerId",
                table: "Sklady_WarehouseWorker",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sklady_WarehouseWorker_Id",
                table: "Sklady_WarehouseWorker",
                column: "Id");
        }
    }
}
