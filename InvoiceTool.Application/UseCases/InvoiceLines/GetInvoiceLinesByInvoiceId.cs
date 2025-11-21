using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.InvoiceLines;

public interface IGetInvoiceLinesByInvoiceId
{
    Task<List<InvoiceLineModel>> ExecuteAsync(int invoiceId);
}

public class GetInvoiceLinesByInvoiceId(IInvoiceLineService invoiceLineService) : IGetInvoiceLinesByInvoiceId
{
    private readonly IInvoiceLineService _invoiceLineService = invoiceLineService;
    public async Task<List<InvoiceLineModel>> ExecuteAsync(int invoiceId)
    {
        var invoiceLines = await _invoiceLineService.GetAllbyInvoiceAsync(invoiceId);

        return invoiceLines ?? new List<InvoiceLineModel>();
    }
}