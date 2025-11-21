using InvoiceTool.Application.Interfaces.UseCases;

namespace InvoiceTool.Application.UseCases.InvoiceLines;
public class InvoiceLineUseCases(
    IGetInvoiceLinesByInvoiceId getInvoiceLinesByInvoiceId,
    ISaveInvoiceLine saveInvoiceLine,
    IDeleteInvoiceLine deleteInvoiceLine) : IInvoiceLineUseCases
{
    public IGetInvoiceLinesByInvoiceId GetInvoiceLinesByInvoiceId { get; } = getInvoiceLinesByInvoiceId;

    public ISaveInvoiceLine SaveInvoiceLine { get; } = saveInvoiceLine;

    public IDeleteInvoiceLine DeleteInvoiceLine { get; } = deleteInvoiceLine;
}
