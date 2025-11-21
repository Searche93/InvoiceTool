using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Invoices;

public interface IGetAllInvoices
{
    Task<List<InvoiceModel>> ExecuteAsync();
}

public class GetAllInvoices(IInvoiceService invoiceService) : IGetAllInvoices
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    public async Task<List<InvoiceModel>> ExecuteAsync()
    {
        var invoices = await _invoiceService.GetAllAsync();

        return invoices ?? new List<InvoiceModel>();
    }
}
