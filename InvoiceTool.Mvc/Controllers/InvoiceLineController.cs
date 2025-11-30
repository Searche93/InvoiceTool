using InvoiceTool.Application.Interfaces.UseCases;
using InvoiceTool.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;

[Authorize]
public class InvoiceLineController(IInvoiceLineUseCases invoiceLineUseCases) : Controller
{
    private readonly IInvoiceLineUseCases _invoiceLineUseCases = invoiceLineUseCases;

    [HttpPost]
    public async Task<IActionResult> GetInvoiceLines(int invoiceId)
    {
        var invoiceLines = await _invoiceLineUseCases.GetInvoiceLinesByInvoiceId.ExecuteAsync(invoiceId);

        return PartialView("~/Views/InvoiceLines/_InvoiceLines.cshtml", invoiceLines);
    }

    public async Task<InvoiceLineModel> Save([FromBody] InvoiceLineModel invoiceLine)
    {
        var savedLined = await _invoiceLineUseCases.SaveInvoiceLine.ExecuteAsync(invoiceLine);

        return savedLined;
    }

    public async Task<bool> Delete(int id)
    {
        var isDeleted = await _invoiceLineUseCases.DeleteInvoiceLine.ExecuteAsync(id);

        return isDeleted;
    }
}
