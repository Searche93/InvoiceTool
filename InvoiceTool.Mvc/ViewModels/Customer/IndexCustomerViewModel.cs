using InvoiceTool.Application.Models;

namespace InvoiceTool.Mvc.ViewModels.Customer;

public class IndexCustomerViewModel
{
    public List<CustomerModel>? Customers { get; set; } = new List<CustomerModel>();
}
