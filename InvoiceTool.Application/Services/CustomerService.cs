using AutoMapper;
using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Mapper;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;

namespace InvoiceTool.Application.Services;

internal class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly IMapper Mapper = AutoMapperConfiguration.CreateMapper();
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CustomerModel?> GetAsync(int id)
    {
        var customer = await _customerRepository.GetAsync(id);

        if (customer == null) return null;


        var customerModel = Mapper.Map<CustomerModel?>(customer);

        return customerModel;
    }

    public async Task<List<CustomerModel>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();

        if (customers == null) return new List<CustomerModel>();


        var listOfCustomers = Mapper.Map<List<CustomerModel>>(customers);

        return listOfCustomers ?? new List<CustomerModel>();
    }

    public async Task<CustomerModel> SaveAsync(CustomerModel customerModel)
    {
        var customer = Mapper.Map<Customer>(customerModel);

        var savedCustomer = await _customerRepository.SaveAsync(customer);

        var savedCustomerModel = Mapper.Map<CustomerModel>(savedCustomer);

        return savedCustomerModel;
    }

    public async Task<bool> DeleteAsync(int customerId)
    {
        return await _customerRepository.DeleteAsync(customerId);
    }
}