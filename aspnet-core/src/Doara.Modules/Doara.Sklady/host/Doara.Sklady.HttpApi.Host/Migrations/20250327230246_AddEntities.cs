using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Sklady.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkladyContainer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkladyContainer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkladyContainerItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkladyContainerItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkladyContainerItem_SkladyContainer_Id",
                        column: x => x.Id,
                        principalTable: "SkladyContainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkladyWarehouseWorker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkladyWarehouseWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkladyWarehouseWorker_SkladyContainer_Id",
                        column: x => x.Id,
                        principalTable: "SkladyContainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkladyContainer_Id",
                table: "SkladyContainer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SkladyContainerItem_Id",
                table: "SkladyContainerItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SkladyWarehouseWorker_Id",
                table: "SkladyWarehouseWorker",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkladyContainerItem");

            migrationBuilder.DropTable(
                name: "SkladyWarehouseWorker");

            migrationBuilder.DropTable(
                name: "SkladyContainer");
        }
    }
}
