using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.Invoices;

namespace InvoiceTool.Application.UseCases.InvoiceLines;

public class SaveInvoiceLine(
    IInvoiceLineService invoiceLineService,
    CalculateInvoice calculateInvoice,
    GetInvoiceById getInvoiceById,
    EditInvoice editInvoice
)
{
    private readonly IInvoiceLineService _invoiceLineService = invoiceLineService;
    private readonly CalculateInvoice _calculateInvoice = calculateInvoice;
    private readonly GetInvoiceById _getInvoiceById = getInvoiceById;
    private readonly EditInvoice _editInvoice = editInvoice;

    public async Task<InvoiceLineModel> Execute(InvoiceLineModel invoiceLine)
    {
        var savedLine = await _invoiceLineService.SaveAsync(invoiceLine);

        var invoice = await _getInvoiceById.Execute(invoiceLine.InvoiceId);

        if (invoice?.InvoiceLines == null)
            return savedLine;
        

        var calculatedInvoice = _calculateInvoice.Execute(invoice, invoice.InvoiceLines);

        if (calculatedInvoice == null || calculatedInvoice.InvoiceLines == null)
            return savedLine;
        

        await _editInvoice.Execute(calculatedInvoice, calculatedInvoice.InvoiceLines);
  
        return savedLine;
    }
}