using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceTool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SettingsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyPostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyPhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyWebsite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyCocNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyVatNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyIban = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceBottomContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "CompanyAddress", "CompanyCity", "CompanyCocNumber", "CompanyEmail", "CompanyIban", "CompanyLogo", "CompanyName", "CompanyPhoneNumber", "CompanyPostalCode", "CompanyVatNumber", "CompanyWebsite", "InvoiceBottomContent" },
                values: new object[] { new Guid("b9c6ed20-13e0-4bb1-b074-556d9fae7f19"), "", "", "", "", "", "", "", "", "", "", "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
