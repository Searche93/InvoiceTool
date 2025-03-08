using InvoiceTool.Application.Models;

namespace InvoiceTool.Mvc.ViewModels.Invoice;

public class IndexInvoiceViewModel
{
    public List<InvoiceModel>? Invoices { get; set; } = new List<InvoiceModel>();
}
