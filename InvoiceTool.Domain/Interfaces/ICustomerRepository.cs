﻿using InvoiceTool.Domain.Entities;

namespace InvoiceTool.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get a single customer by its <paramref name="id"/> id
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
    }
}
