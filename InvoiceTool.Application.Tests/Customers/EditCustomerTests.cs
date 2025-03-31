using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.Customers;
using Moq;

namespace InvoiceTool.Application.Tests.Customers;

public class EditCustomerTests
{
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
}