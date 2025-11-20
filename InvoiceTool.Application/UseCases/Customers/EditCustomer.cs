using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
namespace InvoiceTool.Application.UseCases.Customers;

public interface IEditCustomer
{
    Task<CustomerModel?> ExecuteAsync(CustomerModel customer);
}

public class EditCustomer(ICustomerService customerService) : IEditCustomer
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<CustomerModel?> ExecuteAsync(CustomerModel customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");

        var updatedCustomer = await _customerService.SaveAsync(customer);

        return updatedCustomer;
    }
}