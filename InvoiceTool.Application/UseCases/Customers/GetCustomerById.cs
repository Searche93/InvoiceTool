using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Customers;

public interface IGetCustomerById
{
    Task<CustomerModel?> ExecuteAsync(int id);
}

public class GetCustomerById(ICustomerService customerService) : IGetCustomerById
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<CustomerModel?> ExecuteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id cannot be zero or under.");

        var customer = await _customerService.GetAsync(id);

        return customer;
    }
}
