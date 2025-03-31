using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Customers;

public class CreateCustomer(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<CustomerModel?> Execute(CustomerModel customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");

        var createdCustomer = await _customerService.SaveAsync(customer);

        return createdCustomer;
    }
}
