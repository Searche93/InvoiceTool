using InvoiceTool.Application.Interfaces;

namespace InvoiceTool.Application.UseCases.InvoiceLines;

public class DeleteInvoiceLine(IInvoiceLineService invoiceLineService)
{
    private readonly IInvoiceLineService _invoiceLineService = invoiceLineService;

    // Todo 
    // - This method still needs some fixes. After deleting a line. Update the invoice prices
    public async Task<bool> Execute(int id)
    {
        var isDeleted = await _invoiceLineService.DeleteAsync(id);

        return isDeleted;
    }
}