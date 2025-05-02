using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Sklady.Migrations
{
    /// <inheritdoc />
    public partial class DbSetStockMovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_Sklady_ContainerItem_ContainerItemId",
                table: "StockMovement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockMovement",
                table: "StockMovement");

            migrationBuilder.RenameTable(
                name: "StockMovement",
                newName: "Sklady_StockMovement");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovement_Id",
                table: "Sklady_StockMovement",
                newName: "IX_Sklady_StockMovement_Id");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovement_ContainerItemId",
                table: "Sklady_StockMovement",
                newName: "IX_Sklady_StockMovement_ContainerItemId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Sklady_StockMovement",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sklady_StockMovement",
                table: "Sklady_StockMovement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sklady_StockMovement_Sklady_ContainerItem_ContainerItemId",
                table: "Sklady_StockMovement",
                column: "ContainerItemId",
                principalTable: "Sklady_ContainerItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sklady_StockMovement_Sklady_ContainerItem_ContainerItemId",
                table: "Sklady_StockMovement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sklady_StockMovement",
                table: "Sklady_StockMovement");

            migrationBuilder.RenameTable(
                name: "Sklady_StockMovement",
                newName: "StockMovement");

            migrationBuilder.RenameIndex(
                name: "IX_Sklady_StockMovement_Id",
                table: "StockMovement",
                newName: "IX_StockMovement_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Sklady_StockMovement_ContainerItemId",
                table: "StockMovement",
                newName: "IX_StockMovement_ContainerItemId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StockMovement",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockMovement",
                table: "StockMovement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovement_Sklady_ContainerItem_ContainerItemId",
                table: "StockMovement",
                column: "ContainerItemId",
                principalTable: "Sklady_ContainerItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
