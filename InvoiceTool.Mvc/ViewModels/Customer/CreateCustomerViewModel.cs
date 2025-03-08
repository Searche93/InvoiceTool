using InvoiceTool.Application.Models;

namespace InvoiceTool.Mvc.ViewModels.Customer;

public class CreateCustomerViewModel
{
    public CustomerModel? Customer { get; set; } = new CustomerModel();
}
