using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
namespace InvoiceTool.Application.UseCases.Customers;

public class EditCustomer(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<CustomerModel?> Execute(CustomerModel customer)
    {
        var updatedCustomer = await _customerService.SaveAsync(customer);

        return updatedCustomer;
    }
}