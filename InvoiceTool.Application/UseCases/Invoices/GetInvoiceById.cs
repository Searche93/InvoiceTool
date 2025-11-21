using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Invoices;

public interface IGetInvoiceById
{
    Task<InvoiceModel?> ExecuteAsync(int id);
}

public class GetInvoiceById(IInvoiceService invoiceService) : IGetInvoiceById
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    public async Task<InvoiceModel?> ExecuteAsync(int id)
    {
        var invoice = await _invoiceService.GetAsync(id);

        return invoice;
    }
}
