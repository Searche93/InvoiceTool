using InvoiceTool.Application.Models;

namespace InvoiceTool.Mvc.ViewModels.Invoice;

public class DownloadPdfViewModel
{
    public InvoiceModel Invoice { get; set; } = new InvoiceModel();
    public CustomerModel Customer { get; set; } = new CustomerModel();
}
