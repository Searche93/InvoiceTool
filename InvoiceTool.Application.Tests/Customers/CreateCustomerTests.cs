using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.Customers;
using Moq;

namespace InvoiceTool.Application.Tests.Customers;

public class CreateCustomerTests
{
    [Fact]
    public async Task Handle_ValidCustomer_CreatesCustomerSuccesfully()
    {
        var customerServiceMock = new Mock<ICustomerService>();

        var createCustomerUseCase = new CreateCustomer(customerServiceMock.Object);

        var customer = new CustomerModel { Id = 1, Name = "Serge Zant" };

        await createCustomerUseCase.Execute(customer);

        customerServiceMock.Verify(x => x.SaveAsync(customer), Times.Once);
    }

    [Fact]
    public async Task Handle_CreateCustomerNull_ThrowsNullException()
    {
        var customerServiceMock = new Mock<ICustomerService>();

        var createCustomerUseCase = new CreateCustomer(customerServiceMock.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(() => createCustomerUseCase.Execute(null));
    }

    [Fact]
    public async Task Handle_ValidCustomer_EditCustomerSuccesfully()
    {
        var customerServiceMock = new Mock<ICustomerService>();

        var editCustomerUseCase = new EditCustomer(customerServiceMock.Object);

        var customer = new CustomerModel { Id = 1, Name = "Serge Zant" };

        await editCustomerUseCase.Execute(customer);

        customerServiceMock.Verify(x => x.SaveAsync(customer), Times.Once);
    }

    [Fact]
    public async Task Handle_EditCustomerNull_ThrowsNullException()
    {
        var customerServiceMock = new Mock<ICustomerService>();

        var editCustomerUseCase = new EditCustomer(customerServiceMock.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(() => editCustomerUseCase.Execute(null));
    }


    // Todo: Add tests for the following use cases:

    // GetCustomerById
    // Fail on invalid id

    // GetAllCustomers
    // Return empty list if no customers

    // DeleteCustomer
    // Fail on invalid id
}