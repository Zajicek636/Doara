using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doara.Ucetnictvi.Migrations
{
    /// <inheritdoc />
    public partial class Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ucetnictvi_Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucetnictvi_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ucetnictvi_Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucetnictvi_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ucetnictvi_Address_Ucetnictvi_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Ucetnictvi_Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ucetnictvi_Subject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ic = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Dic = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    IsVatPayer = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucetnictvi_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ucetnictvi_Subject_Ucetnictvi_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Ucetnictvi_Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ucetnictvi_Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaxDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalNetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalGrossAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentTerms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VatRate = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    VariableSymbol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ConstantSymbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SpecificSymbol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucetnictvi_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ucetnictvi_Invoice_Ucetnictvi_Subject_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Ucetnictvi_Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ucetnictvi_Invoice_Ucetnictvi_Subject_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Ucetnictvi_Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ucetnictvi_InvoiceItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucetnictvi_InvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ucetnictvi_InvoiceItem_Ucetnictvi_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Ucetnictvi_Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ucetnictvi_Address_CountryId",
                table: "Ucetnictvi_Address",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ucetnictvi_Address_Id",
                table: "Ucetnictvi_Address",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ucetnictvi_Country_Id",
                table: "Ucetnictvi_Country",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ucetnictvi_Invoice_CustomerId",
                table: "Ucetnictvi_Invoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ucetnictvi_Invoice_Id",
                table: "Ucetnictvi_Invoice",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ucetnictvi_Invoice_SupplierId",
                table: "Ucetnictvi_Invoice",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Ucetnictvi_InvoiceItem_Id",
                table: "Ucetnictvi_InvoiceItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ucetnictvi_InvoiceItem_InvoiceId",
                table: "Ucetnictvi_InvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ucetnictvi_Subject_AddressId",
                table: "Ucetnictvi_Subject",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Ucetnictvi_Subject_Id",
                table: "Ucetnictvi_Subject",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ucetnictvi_InvoiceItem");

            migrationBuilder.DropTable(
                name: "Ucetnictvi_Invoice");

            migrationBuilder.DropTable(
                name: "Ucetnictvi_Subject");

            migrationBuilder.DropTable(
                name: "Ucetnictvi_Address");

            migrationBuilder.DropTable(
                name: "Ucetnictvi_Country");
        }
    }
}
