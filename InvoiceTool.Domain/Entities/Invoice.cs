namespace InvoiceTool.Domain.Entities;
public class Invoice
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal NetPrice { get; set; }
    public decimal TaxPrice { get; set; }
    public decimal GrossPrice { get; set; }
    public Customer Customer { get; set; } = new();
    public List<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
}