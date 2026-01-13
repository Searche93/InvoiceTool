using InvoiceTool.Application.Models;

namespace InvoiceTool.Mvc.ViewModels.Search;

public class SearchResultsViewModel
{
    public string SearchInput { get; set; } = string.Empty;
    public List<InvoiceModel> Invoices { get; set; } = new();
    public List<CustomerModel> Customers { get; set; } = new();
}
