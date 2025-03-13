using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceTool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxPercentageToInvoiceLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaxPercentage",
                table: "InvoiceLines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxPercentage",
                table: "InvoiceLines");
        }
    }
}
