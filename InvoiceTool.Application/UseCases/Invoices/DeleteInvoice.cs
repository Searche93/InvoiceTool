using InvoiceTool.Application.Interfaces;

namespace InvoiceTool.Application.UseCases.Invoices;

public interface IDeleteInvoice
{
    Task<bool> ExecuteAsync(int id);
}

public class DeleteInvoice(IInvoiceService invoiceService) : IDeleteInvoice
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    public async Task<bool> ExecuteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id cannot be zero or under.");

        var isDeleted = await _invoiceService.DeleteAsync(id);

        return isDeleted;
    }
}