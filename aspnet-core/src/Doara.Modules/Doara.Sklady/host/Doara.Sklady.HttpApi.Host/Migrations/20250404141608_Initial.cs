using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Sklady.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sklady_Container",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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
                    table.PrimaryKey("PK_Sklady_Container", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sklady_ContainerItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    State = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    QuantityType = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    PurchaseUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RealPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PresentationPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Markup = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarkupRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sklady_ContainerItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sklady_ContainerItem_Sklady_Container_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Sklady_Container",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sklady_ContainerItem_Sklady_Container_Id",
                        column: x => x.Id,
                        principalTable: "Sklady_Container",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sklady_WarehouseWorker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sklady_WarehouseWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sklady_WarehouseWorker_Sklady_Container_Id",
                        column: x => x.Id,
                        principalTable: "Sklady_Container",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sklady_Container_Id",
                table: "Sklady_Container",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sklady_ContainerItem_ContainerId",
                table: "Sklady_ContainerItem",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sklady_ContainerItem_Id",
                table: "Sklady_ContainerItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sklady_WarehouseWorker_Id",
                table: "Sklady_WarehouseWorker",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sklady_ContainerItem");

            migrationBuilder.DropTable(
                name: "Sklady_WarehouseWorker");

            migrationBuilder.DropTable(
                name: "Sklady_Container");
        }
    }
}
