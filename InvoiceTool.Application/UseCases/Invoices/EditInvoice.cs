using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Invoices;

public interface IEditInvoice
{
    Task<InvoiceModel?> ExecuteAsync(InvoiceModel invoice, List<InvoiceLineModel> invoiceLines);
}

public class EditInvoice(IInvoiceService invoiceService, ICalculateInvoice calculateInvoice) : IEditInvoice
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    private readonly ICalculateInvoice _calculateInvoice = calculateInvoice;

    public async Task<InvoiceModel?> ExecuteAsync(InvoiceModel invoice, List<InvoiceLineModel> invoiceLines)
    {
        invoice = _calculateInvoice.Execute(invoice, invoiceLines);

        return await _invoiceService.SaveAsync(invoice);
    }
}
