using FluentAssertions;
using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.Customers;
using Moq;

namespace InvoiceTool.Application.Tests.Customers;

public class GetAllCustomersTests
{
    [Fact]
    public async Task GetAllCustomers_Handle_ReturnsListOfCustomers()
    {
        // Arrange
        var mockedService = new Mock<ICustomerService>();

        var expectedResult = new List<CustomerModel>
        {
            new() { Name = "Serge", CompanyName = "Searche" },
            new() { Name = "Esther", CompanyName = "Createdbyes" },
            new() { Name = "Luuk" }
        };

        mockedService.Setup(s => s.GetAllAsync()).ReturnsAsync(expectedResult);

        var useCase = new GetAllCustomers(mockedService.Object);

        // Act
        var result = await useCase.ExecuteAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Fact]
    public async Task GetAllCustomers_Handle_ReturnsEmptyListOfCustomerModel()
    {
        // Arrange
        var mockedService = new Mock<ICustomerService>();

        mockedService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<CustomerModel>());

        var useCase = new GetAllCustomers(mockedService.Object);

        // Act
        var result = await useCase.ExecuteAsync();

        // Assert
        result.Should().BeEquivalentTo(new List<CustomerModel>());
    }
}