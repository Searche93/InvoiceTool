using InvoiceTool.Application.Interfaces;

namespace InvoiceTool.Application.UseCases.Invoices;

public class DeleteInvoice(IInvoiceService invoiceService)
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    public async Task<bool> Execute(int id)
    {
        var isDeleted = await _invoiceService.DeleteAsync(id);

        return isDeleted;
    }
}