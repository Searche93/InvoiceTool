using InvoiceTool.Application.Models;

namespace InvoiceTool.Mvc.ViewModels.Customer;

public class EditCustomerViewModel
{
    public CustomerModel? Customer { get; set; } = new CustomerModel();
}
