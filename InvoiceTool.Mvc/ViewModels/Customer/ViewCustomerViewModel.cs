using InvoiceTool.Application.Models;

namespace InvoiceTool.Mvc.ViewModels.Customer;

public class ViewCustomerViewModel
{
    public CustomerModel? Customer { get; set; } = new CustomerModel();
}
