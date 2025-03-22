using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Invoices;

public class GetInvoiceById(IInvoiceService invoiceService)
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    public async Task<InvoiceModel?> Execute(int id)
    {
        var invoice = await _invoiceService.GetAsync(id);

        return invoice;
    }
}
