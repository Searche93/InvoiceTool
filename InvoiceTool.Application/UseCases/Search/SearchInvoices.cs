using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Search;

public interface ISearchInvoices
{
    Task<List<InvoiceModel>> Execute(string searchInput);
}

public class SearchInvoices(IInvoiceService invoiceService) : ISearchInvoices
{
    private readonly IInvoiceService _invoiceService = invoiceService;

    public async Task<List<InvoiceModel>> Execute(string searchInput)
    {
        var results = await _invoiceService.SearchAsync(searchInput);

        return results;
    }
}
