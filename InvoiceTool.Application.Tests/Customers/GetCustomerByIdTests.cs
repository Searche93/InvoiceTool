using FluentAssertions;
using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.Customers;
using Moq;

namespace InvoiceTool.Application.Tests.Customers;

public class GetCustomerByIdTests
{
    [Fact]
    public async Task GetCustomerById_Handle_ReturnsOneCustomer()
    {
        // Arrange
        var mockedService = new Mock<ICustomerService>();

        var expectedResult = new CustomerModel
        {
            Name = "Serge",
            CompanyName = "Searche"
        };

        mockedService.Setup(s => s.GetAsync(1)).ReturnsAsync(expectedResult);

        var useCase = new GetCustomerById(mockedService.Object);

        // Act
        var result = await useCase.ExecuteAsync(1);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task GetCustomerById_Handle_ArgumentException()
    {
        // Arrange
        var mockedService = new Mock<ICustomerService>();

        var useCase = new GetCustomerById(mockedService.Object);

        // Act
        var result = async () => await useCase.ExecuteAsync(0);

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }
}