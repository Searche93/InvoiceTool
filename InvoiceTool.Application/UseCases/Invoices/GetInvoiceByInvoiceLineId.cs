using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Invoices;

public class GetInvoiceByInvoiceLineIdAsync(IInvoiceService invoiceService)
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    public async Task<InvoiceModel?> Execute(int invoiceLindId)
    {
        var invoice = await _invoiceService.GetInvoiceByInvoiceLineIdAsync(invoiceLindId);

        return invoice;
    }
}
