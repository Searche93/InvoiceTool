using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.InvoiceLines;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;

public class InvoiceLineController(
    GetInvoiceLinesByInvoiceId getInvoiceLinesByInvoiceId,
    SaveInvoiceLine saveInvoiceLine,
    DeleteInvoiceLine deleteInvoiceLine
) : Controller
{
    private readonly GetInvoiceLinesByInvoiceId _getInvoiceLinesByInvoiceId = getInvoiceLinesByInvoiceId;
    private readonly SaveInvoiceLine _saveInvoiceLine = saveInvoiceLine;
    private readonly DeleteInvoiceLine _deleteInvoiceLine = deleteInvoiceLine;


    [HttpPost]
    public async Task<IActionResult> GetInvoiceLines(int invoiceId)
    {
        var invoiceLines = await _getInvoiceLinesByInvoiceId.Execute(invoiceId);

        return PartialView("~/Views/InvoiceLines/_InvoiceLines.cshtml", invoiceLines);
    }

    public async Task<InvoiceLineModel> Save([FromBody] InvoiceLineModel invoiceLine)
    {
        var savedLined = await _saveInvoiceLine.Execute(invoiceLine);

        return savedLined;
    }

    public async Task<bool> Delete(int id)
    {
        var isDeleted = await _deleteInvoiceLine.Execute(id);

        return isDeleted;
    }
}
