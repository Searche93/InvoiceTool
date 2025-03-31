using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.Interfaces;

public interface ICustomerService
{
    /// <summary>
    /// Get a customer by its <paramref name="id"/>
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Customer model</returns>
    Task<CustomerModel?> GetAsync(int id);

    /// <summary>
    /// Get a list of customers
    /// </summary>
    /// <returns>List of customer model</returns>
    Task<List<CustomerModel>> GetAllAsync();

    /// <summary>
    /// Save a customer
    /// </summary>
    /// <param name="customerModel"></param>
    /// <returns><paramref name="customerModel"/></returns>
    Task<CustomerModel> SaveAsync(CustomerModel customerModel);

    /// <summary>
    /// Delete a customer by its <paramref name="customerId"/>
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns>True if deleted succesfull</returns>
    Task<bool> DeleteAsync(int customerId);
}
