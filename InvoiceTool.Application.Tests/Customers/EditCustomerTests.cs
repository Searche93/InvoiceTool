using FluentAssertions;
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
        // Arrange
        var mockedService = new Mock<ICustomerService>();

        mockedService
            .Setup(s => s.SaveAsync(It.IsAny<CustomerModel>()))
            .ReturnsAsync((CustomerModel input) => input);

        var useCase = new CreateCustomer(mockedService.Object);

        var customer = new CustomerModel()
        {
            Name = "Serge",
            CompanyName = "Searche",
            Address = "Changed Adres 5",
            PostalCode = "6789 XY",
            City = "London",
        };

        // Act
        var result = await useCase.ExecuteAsync(customer);

        // Assert
        result.Should().BeEquivalentTo(customer);
    }

    [Fact]
    public async Task Handle_EditCustomerNull_ThrowsNullException()
    {
        // Arrange
        var mockedService = new Mock<ICustomerService>();

        var useCase = new CreateCustomer(mockedService.Object);

        // Act
        var result = async () => await useCase.ExecuteAsync(null!);

        // Assert
        await result.Should().ThrowAsync<ArgumentNullException>();
    }
}