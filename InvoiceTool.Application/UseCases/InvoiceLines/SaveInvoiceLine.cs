using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Interfaces.UseCases;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.InvoiceLines;

public interface ISaveInvoiceLine
{
    Task<InvoiceLineModel> ExecuteAsync(InvoiceLineModel invoiceLine);
}

public class SaveInvoiceLine(IInvoiceLineService invoiceLineService, IInvoiceUseCases invoiceUseCases) : ISaveInvoiceLine
{
    private readonly IInvoiceLineService _invoiceLineService = invoiceLineService;
    private readonly IInvoiceUseCases _invoiceUseCases = invoiceUseCases;

    public async Task<InvoiceLineModel> ExecuteAsync(InvoiceLineModel invoiceLine)
    {
        var savedLine = await _invoiceLineService.SaveAsync(invoiceLine);

        var invoice = await _invoiceUseCases.GetInvoiceById.ExecuteAsync(invoiceLine.InvoiceId);

        if (invoice?.InvoiceLines == null)
            return savedLine;


        var calculatedInvoice = _invoiceUseCases.CalculateInvoice.Execute(invoice, invoice.InvoiceLines);

        if (calculatedInvoice == null || calculatedInvoice.InvoiceLines == null)
            return savedLine;


        await _invoiceUseCases.EditInvoice.ExecuteAsync(calculatedInvoice, calculatedInvoice.InvoiceLines);

        return savedLine;
    }
}