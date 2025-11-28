using InvoiceTool.Domain.Enums;
using System.Globalization;

namespace InvoiceTool.Application.Models;

public class InvoiceModel
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public string DateValue => Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
    public DateTime ExpirationDate { get; set; } = DateTime.Now.AddMonths(1);
    public string ExpirationDateValue => ExpirationDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
    public decimal NetPrice { get; set; }
    public decimal TaxPrice { get; set; }
    public decimal GrossPrice { get; set; }

    public int CustomerId { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public List<InvoiceLineModel>? InvoiceLines { get; set; }
}
