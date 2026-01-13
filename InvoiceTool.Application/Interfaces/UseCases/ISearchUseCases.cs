using InvoiceTool.Application.UseCases.Search;

namespace InvoiceTool.Application.Interfaces.UseCases;

public interface ISearchUseCases
{
    ISearchCustomer SearchCustomer { get; }
    ISearchInvoices SearchInvoices { get; }
}
