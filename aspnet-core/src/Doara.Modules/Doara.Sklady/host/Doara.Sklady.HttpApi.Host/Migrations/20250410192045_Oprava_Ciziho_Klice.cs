using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Sklady.Migrations
{
    /// <inheritdoc />
    public partial class Oprava_Ciziho_Klice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sklady_ContainerItem_Sklady_Container_ContainerId",
                table: "Sklady_ContainerItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Sklady_ContainerItem_Sklady_Container_Id",
                table: "Sklady_ContainerItem");

            migrationBuilder.AddForeignKey(
                name: "FK_Sklady_ContainerItem_Sklady_Container_ContainerId",
                table: "Sklady_ContainerItem",
                column: "ContainerId",
                principalTable: "Sklady_Container",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sklady_ContainerItem_Sklady_Container_ContainerId",
                table: "Sklady_ContainerItem");

            migrationBuilder.AddForeignKey(
                name: "FK_Sklady_ContainerItem_Sklady_Container_ContainerId",
                table: "Sklady_ContainerItem",
                column: "ContainerId",
                principalTable: "Sklady_Container",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sklady_ContainerItem_Sklady_Container_Id",
                table: "Sklady_ContainerItem",
                column: "Id",
                principalTable: "Sklady_Container",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
