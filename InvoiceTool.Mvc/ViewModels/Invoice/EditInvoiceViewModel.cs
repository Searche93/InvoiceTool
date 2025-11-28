using InvoiceTool.Application.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceTool.Mvc.ViewModels.Invoice;

public class EditInvoiceViewModel
{
    public InvoiceModel Invoice { get; set; } = new InvoiceModel();
    public List<CustomerModel> Customers { get; set; } = new List<CustomerModel>();
    
    [BindNever]
    public IEnumerable<SelectListItem> PaymentStatusList { get; set; } = Enumerable.Empty<SelectListItem>();
}
