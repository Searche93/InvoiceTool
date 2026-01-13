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

    /// <summary>
    /// Asynchronously searches for customers whose details match the specified input string.
    /// </summary>
    /// <param name="searchInput">The text to search for within customer records. This may include a name, email address, or other identifying
    /// information. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of customers matching the
    /// search criteria. The list will be empty if no matches are found.</returns>
    Task<List<CustomerModel>> SearchAsync(string searchInput);
}
