using InvoiceTool.Application.Models;

namespace InvoiceTool.Mvc.ViewModels;

public class ViewInvoiceViewModel
{
    public InvoiceModel? Invoice { get; set; } = new InvoiceModel();
}
