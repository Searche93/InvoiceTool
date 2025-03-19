namespace InvoiceTool.Application.Models;

public class InvoiceLineModel
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int TaxPercentage { get; set; }
    public decimal Quantity { get; set; }
    public string NetPrice => (UnitPrice * Quantity).ToString("F2");
}
