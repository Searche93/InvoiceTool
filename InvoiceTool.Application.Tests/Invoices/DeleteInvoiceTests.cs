using FluentAssertions;
using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.UseCases.Invoices;
using Moq;

namespace InvoiceTool.Application.Tests.Invoices;

public class DeleteInvoiceTests
{
    [Fact]
    public async Task DeleteInvoice_Handle_InvoiceDeletedSuccessfull()
    {
        // Arrange
        var mockedService = new Mock<IInvoiceService>();

        mockedService
            .Setup(s => s.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        var useCase = new DeleteInvoice(mockedService.Object);

        // Act
        var result = await useCase.ExecuteAsync(1);

        // Assert
        result.Should().Be(true);
    }


    [Fact]
    public async Task DeleteInvoice_Handle_InvoiceDeletedUnSuccessfull()
    {
        // Arrange
        var mockedService = new Mock<IInvoiceService>();

        mockedService
            .Setup(s => s.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        var useCase = new DeleteInvoice(mockedService.Object);

        // Act
        var result = await useCase.ExecuteAsync(1);

        // Assert
        result.Should().Be(false);
    }


    [Fact]
    public async Task DeleteInvoice_Handle_ArgumentException()
    {
        // Arrange
        var mockedService = new Mock<IInvoiceService>();

        mockedService
            .Setup(s => s.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        var useCase = new DeleteInvoice(mockedService.Object);

        // Act
        var result = async () => await useCase.ExecuteAsync(0);

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }
}
