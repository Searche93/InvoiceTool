using InvoiceTool.Application.Models;

namespace InvoiceTool.Mvc.ViewModels.Invoice;

public class DownloadPdfViewModel
{
    public InvoiceModel Invoice { get; set; } = new InvoiceModel();
    public CustomerModel Customer { get; set; } = new CustomerModel();
    public SettingsModel Settings { get; set; } = new SettingsModel();
}
