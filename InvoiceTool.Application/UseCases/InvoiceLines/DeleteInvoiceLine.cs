using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.UseCases.Invoices;

namespace InvoiceTool.Application.UseCases.InvoiceLines;

public class DeleteInvoiceLine(
    IInvoiceLineService invoiceLineService,
    CalculateInvoice calculateInvoice,
    EditInvoice editInvoice,
    GetInvoiceById getInvoiceById,
    GetInvoiceByInvoiceLineIdAsync getInvoiceByInvoiceLineIdAsync
)
{
    private readonly IInvoiceLineService _invoiceLineService = invoiceLineService;
    private readonly CalculateInvoice _calculateInvoice = calculateInvoice;
    private readonly EditInvoice _editInvoice = editInvoice;
    private readonly GetInvoiceById _getInvoiceById = getInvoiceById;
    private readonly GetInvoiceByInvoiceLineIdAsync _getInvoiceByInvoiceLineIdAsync = getInvoiceByInvoiceLineIdAsync;

    public async Task<bool> Execute(int id)
    {
        var invoice = await _getInvoiceByInvoiceLineIdAsync.Execute(id);

        var isDeleted = await _invoiceLineService.DeleteAsync(id);

        if (invoice == null)
            return isDeleted;

        invoice = await _getInvoiceById.Execute(invoice.Id);

        if (invoice?.InvoiceLines == null)
            return isDeleted;

        var calculatedInvoice = _calculateInvoice.Execute(invoice, invoice.InvoiceLines);

        if (calculatedInvoice == null || calculatedInvoice.InvoiceLines == null)
            return isDeleted;


        await _editInvoice.Execute(calculatedInvoice, calculatedInvoice.InvoiceLines);

        return isDeleted;
    }
}