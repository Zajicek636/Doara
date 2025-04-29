using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Sklady.Migrations
{
    /// <inheritdoc />
    public partial class OpravaVazeb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sklady_WarehouseWorker_Sklady_Container_Id",
                table: "Sklady_WarehouseWorker");

            migrationBuilder.CreateIndex(
                name: "IX_Sklady_WarehouseWorker_ContainerId",
                table: "Sklady_WarehouseWorker",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sklady_WarehouseWorker_Sklady_Container_ContainerId",
                table: "Sklady_WarehouseWorker",
                column: "ContainerId",
                principalTable: "Sklady_Container",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sklady_WarehouseWorker_Sklady_Container_ContainerId",
                table: "Sklady_WarehouseWorker");

            migrationBuilder.DropIndex(
                name: "IX_Sklady_WarehouseWorker_ContainerId",
                table: "Sklady_WarehouseWorker");

            migrationBuilder.AddForeignKey(
                name: "FK_Sklady_WarehouseWorker_Sklady_Container_Id",
                table: "Sklady_WarehouseWorker",
                column: "Id",
                principalTable: "Sklady_Container",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
