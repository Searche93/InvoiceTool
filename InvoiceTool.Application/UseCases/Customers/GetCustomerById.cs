using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Customers;

public class GetCustomerById(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<CustomerModel?> Execute(int id)
    {
        var customer = await _customerService.GetAsync(id);

        return customer;
    }
}
