using InvoiceTool.Application.Interfaces.UseCases;

namespace InvoiceTool.Application.UseCases.Search;

public class SearchUseCases(ISearchCustomer searchCustomers, ISearchInvoices searchInvoices) : ISearchUseCases
{
    public ISearchCustomer SearchCustomer { get; } = searchCustomers;
    public ISearchInvoices SearchInvoices { get; } = searchInvoices;
}