using InvoiceTool.Application.Interfaces.UseCases;

namespace InvoiceTool.Application.UseCases.Customers;
public class CustomerUseCases(
    ICreateCustomer createCustomer,
    IDeleteCustomer deleteCustomer,
    IEditCustomer editCustomer,
    IGetAllCustomers getAllCustomers,
    IGetCustomerById getCustomerById) : ICustomerUseCases
{
    public ICreateCustomer CreateCustomer { get; } = createCustomer;
    public IDeleteCustomer DeleteCustomer { get; } = deleteCustomer;
    public IEditCustomer EditCustomer { get; } = editCustomer;
    public IGetAllCustomers GetAllCustomers { get; } = getAllCustomers;
    public IGetCustomerById GetCustomerById { get; } = getCustomerById;
}
