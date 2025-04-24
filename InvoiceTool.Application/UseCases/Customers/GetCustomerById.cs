using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Customers;

public class GetCustomerById(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<CustomerModel?> Execute(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id cannot be zero or under.");

        var customer = await _customerService.GetAsync(id);

        return customer;
    }
}
