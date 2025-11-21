using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Invoices;

public interface ICreateInvoice
{
    Task<InvoiceModel> ExecuteAsync(InvoiceModel invoice, List<InvoiceLineModel> invoiceLines);
}

public class CreateInvoice(IInvoiceService invoiceService, ICalculateInvoice calculateInvoice) : ICreateInvoice
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    private readonly ICalculateInvoice _calculateInvoice = calculateInvoice;

    public async Task<InvoiceModel> ExecuteAsync(InvoiceModel invoice, List<InvoiceLineModel> invoiceLines)
    {
        invoice = _calculateInvoice.Execute(invoice, invoiceLines);

        var createdInvoice = await _invoiceService.SaveAsync(invoice);

        return createdInvoice;
    }
}
