using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.InvoiceLines;

public class GetInvoiceLinesByInvoiceId(IInvoiceLineService invoiceLineService)
{
    private readonly IInvoiceLineService _invoiceLineService = invoiceLineService;
    public async Task<List<InvoiceLineModel>> Execute(int invoiceId)
    {
        var invoiceLines = await _invoiceLineService.GetAllbyInvoiceAsync(invoiceId);

        return invoiceLines ?? new List<InvoiceLineModel>();
    }
}