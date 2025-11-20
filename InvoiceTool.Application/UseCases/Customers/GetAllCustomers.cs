using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Customers;

public interface IGetAllCustomers
{
    Task<List<CustomerModel>> ExecuteAsync();
}

public class GetAllCustomers(ICustomerService customerService) : IGetAllCustomers
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<List<CustomerModel>> ExecuteAsync()
    {
        var customers = await _customerService.GetAllAsync();

        return customers ?? new List<CustomerModel>();
    }
}
