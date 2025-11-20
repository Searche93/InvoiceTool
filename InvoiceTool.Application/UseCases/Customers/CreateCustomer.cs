using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Customers;

public interface ICreateCustomer
{
    Task<CustomerModel?> ExecuteAsync(CustomerModel customer);
}

public class CreateCustomer(ICustomerService customerService) : ICreateCustomer
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<CustomerModel?> ExecuteAsync(CustomerModel customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");

        var createdCustomer = await _customerService.SaveAsync(customer);

        return createdCustomer;
    }
}
