using InvoiceTool.Application.UseCases.Customers;

namespace InvoiceTool.Application.Interfaces.UseCases;
public interface ICustomerUseCases
{
    ICreateCustomer CreateCustomer { get; }
    IDeleteCustomer DeleteCustomer { get; }
    IEditCustomer EditCustomer { get; }
    IGetAllCustomers GetAllCustomers { get; }
    IGetCustomerById GetCustomerById { get; }
}
