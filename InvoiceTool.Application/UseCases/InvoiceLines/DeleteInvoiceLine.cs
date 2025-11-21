using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Interfaces.UseCases;

namespace InvoiceTool.Application.UseCases.InvoiceLines;

public interface IDeleteInvoiceLine
{
    Task<bool> ExecuteAsync(int id);
}

public class DeleteInvoiceLine(IInvoiceLineService invoiceLineService, IInvoiceUseCases invoiceUseCases) : IDeleteInvoiceLine
{
    private readonly IInvoiceLineService _invoiceLineService = invoiceLineService;
    private readonly IInvoiceUseCases _invoiceUseCases = invoiceUseCases;

    public async Task<bool> ExecuteAsync(int id)
    {
        var invoice = await _invoiceUseCases.GetInvoiceByInvoiceLineId.ExecuteAsync(id);

        var isDeleted = await _invoiceLineService.DeleteAsync(id);

        if (invoice == null)
            return isDeleted;

        invoice = await _invoiceUseCases.GetInvoiceById.ExecuteAsync(invoice.Id);

        if (invoice?.InvoiceLines == null)
            return isDeleted;

        var calculatedInvoice = _invoiceUseCases.CalculateInvoice.Execute(invoice, invoice.InvoiceLines);

        if (calculatedInvoice == null || calculatedInvoice.InvoiceLines == null)
            return isDeleted;


        await _invoiceUseCases.EditInvoice.ExecuteAsync(calculatedInvoice, calculatedInvoice.InvoiceLines);

        return isDeleted;
    }
}