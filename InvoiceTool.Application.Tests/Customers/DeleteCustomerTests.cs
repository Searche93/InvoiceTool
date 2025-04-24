using FluentAssertions;
using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.UseCases.Customers;
using Moq;

namespace InvoiceTool.Application.Tests.Customers;

public class DeleteCustomerTests
{
    [Fact]
    public async Task DeleteCustomer_Handle_CustomerDeletedSuccessfull()
    {
        // Arrange
        var mockedService = new Mock<ICustomerService>();

        mockedService
            .Setup(s => s.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        var useCase = new DeleteCustomer(mockedService.Object);

        // Act
        var result = await useCase.Execute(1);

        // Assert
        result.Should().Be(true);
    }


    [Fact]
    public async Task DeleteCustomer_Handle_CustomerDeletedUnSuccessfull()
    {
        // Arrange
        var mockedService = new Mock<ICustomerService>();

        mockedService
            .Setup(s => s.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        var useCase = new DeleteCustomer(mockedService.Object);

        // Act
        var result = await useCase.Execute(1);

        // Assert
        result.Should().Be(false);
    }


    [Fact]
    public async Task DeleteCustomer_Handle_ArgumentException()
    {
        // Arrange
        var mockedService = new Mock<ICustomerService>();

        mockedService
            .Setup(s => s.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        var useCase = new DeleteCustomer(mockedService.Object);

        // Act
        var result = async () => await useCase.Execute(0);

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }
}
