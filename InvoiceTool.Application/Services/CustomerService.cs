using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;
using Mapster;

namespace InvoiceTool.Application.Services;

internal class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CustomerModel?> GetAsync(int id)
    {
        var customer = await _customerRepository.GetAsync(id);

        if (customer == null) return null;


        var customerModel = customer.Adapt<CustomerModel?>();

        return customerModel;
    }

    public async Task<List<CustomerModel>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();

        if (customers == null) return new List<CustomerModel>();


        var listOfCustomers = customers.Adapt<List<CustomerModel>>();

        return listOfCustomers ?? new List<CustomerModel>();
    }

    public async Task<CustomerModel> SaveAsync(CustomerModel customerModel)
    {
        var customer = customerModel.Adapt<Customer>();

        var savedCustomer = await _customerRepository.SaveAsync(customer);

        var savedCustomerModel = savedCustomer.Adapt<CustomerModel>();

        return savedCustomerModel;
    }

    public async Task<bool> DeleteAsync(int customerId)
    {
        return await _customerRepository.DeleteAsync(customerId);
    }
}