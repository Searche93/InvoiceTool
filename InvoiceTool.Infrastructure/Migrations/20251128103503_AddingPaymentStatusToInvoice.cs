using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceTool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingPaymentStatusToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Invoices");
        }
    }
}
