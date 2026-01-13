using InvoiceTool.Domain.Entities;

namespace InvoiceTool.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get a single customer by its <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CustomerModel</returns>
        Task<Customer?> GetAsync(int id);

        /// <summary>
        /// Get a list of customers
        /// </summary>
        /// <returns>List of CustomerModel</returns>
        Task<List<Customer>> GetAllAsync();

        /// <summary>
        /// Add or update a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Updated entity</returns>
        Task<Customer> SaveAsync(Customer customer);

        /// <summary>
        /// Delete a customer by its <paramref name="customerId"/>
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>True if deleted succesfull</returns>
        Task<bool> DeleteAsync(int customerId);

        /// <summary>
        /// Asynchronously searches for customers whose details match the specified input string.
        /// </summary>
        /// <param name="searchInput">The text to search for within customer records. This value can include partial names, email addresses, or
        /// other identifying information. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of customers matching the
        /// search criteria. If no customers are found, the list will be empty.</returns>
        Task<List<Customer>> SearchAsync(string searchInput);
    }
}
