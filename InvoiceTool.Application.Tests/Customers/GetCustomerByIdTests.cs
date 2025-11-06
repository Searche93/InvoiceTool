using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.Customers;
using Moq;

namespace InvoiceTool.Application.Tests.Customers;

public class GetCustomerByIdTests
{
    [Fact]
    public async Task Handle_ValidCustomer_GetCustomerSuccesfully()
    {
        var customerServiceMock = new Mock<ICustomerService>();

        var getCustomerByIdUseCase = new GetCustomerById(customerServiceMock.Object);

        await getCustomerByIdUseCase.Execute(1);

        customerServiceMock.Verify(x => x.GetAsync(1), Times.Once);
    }

    [Fact]
    public async Task Handle_InValidCustomer_GetCustomerUnSuccesfull()
    {
        var customerServiceMock = new Mock<ICustomerService>();

        var getCustomerByIdUseCase = new GetCustomerById(customerServiceMock.Object);

        var result = await getCustomerByIdUseCase.Execute(-1);

        //await Assert.True(result is null);
    }
}
