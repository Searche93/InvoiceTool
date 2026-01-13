using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Search;

public interface ISearchCustomer
{
    Task<List<CustomerModel>> Execute(string searchInput);
}

public class SearchCustomer(ICustomerService customerService) : ISearchCustomer
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<List<CustomerModel>> Execute(string searchInput)
    {
        var results = await _customerService.SearchAsync(searchInput);

        return results;
    }
}
