using InvoiceTool.Application.UseCases.InvoiceLines;

namespace InvoiceTool.Application.Interfaces.UseCases;
public interface IInvoiceLineUseCases
{
    IGetInvoiceLinesByInvoiceId GetInvoiceLinesByInvoiceId { get; }
    ISaveInvoiceLine SaveInvoiceLine { get; }
    IDeleteInvoiceLine DeleteInvoiceLine { get; }
}
