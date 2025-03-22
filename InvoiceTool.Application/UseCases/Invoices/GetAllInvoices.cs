using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Invoices;

public class GetAllInvoices(IInvoiceService invoiceService)
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    public async Task<List<InvoiceModel>> Execute()
    {
        var invoices = await _invoiceService.GetAllAsync();

        return invoices ?? new List<InvoiceModel>();
    }
}
