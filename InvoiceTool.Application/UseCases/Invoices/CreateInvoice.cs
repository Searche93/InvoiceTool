using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Invoices;

public class CreateInvoice(IInvoiceService invoiceService, CalculateInvoice calculateInvoice)
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    private readonly CalculateInvoice _calculateInvoice = calculateInvoice;

    public async Task<InvoiceModel> Execute(InvoiceModel invoice, List<InvoiceLineModel> invoiceLines)
    {
        invoice = _calculateInvoice.Execute(invoice, invoiceLines);

        var createdInvoice = await _invoiceService.SaveAsync(invoice);

        return createdInvoice;
    }
}
