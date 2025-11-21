using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Invoices;

public interface IGetInvoiceByInvoiceLineId
{
    Task<InvoiceModel?> ExecuteAsync(int invoiceLindId);
}

public class GetInvoiceByInvoiceLineId(IInvoiceService invoiceService) : IGetInvoiceByInvoiceLineId
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    public async Task<InvoiceModel?> ExecuteAsync(int invoiceLindId)
    {
        var invoice = await _invoiceService.GetInvoiceByInvoiceLineIdAsync(invoiceLindId);

        return invoice;
    }
}
