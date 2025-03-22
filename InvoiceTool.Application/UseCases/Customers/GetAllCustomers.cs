using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Customers;

public class GetAllCustomers(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<List<CustomerModel>> Execute()
    {
        var customers = await _customerService.GetAllAsync();

        return customers ?? new List<CustomerModel>();
    }
}
