using InvoiceTool.Application.Interfaces;

namespace InvoiceTool.Application.UseCases.Customers;

public interface IDeleteCustomer
{
    Task<bool> ExecuteAsync(int id);
}

public class DeleteCustomer(ICustomerService customerService) : IDeleteCustomer
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<bool> ExecuteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id cannot be zero or under.");

        var isDeleted = await _customerService.DeleteAsync(id);

        return isDeleted;
    }
}
