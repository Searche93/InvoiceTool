using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;

public class InvoiceLineController(IInvoiceLineService invoiceLineService) : Controller
{
    private readonly IInvoiceLineService _invoiceLineService = invoiceLineService;


    // Todo 
    // - Use UseCases
    // - Calculate the invoice cost on saving a invoice line

    [HttpPost]
    public async Task<IActionResult> GetInvoiceLines(int invoiceId)
    {
        var invoiceLines = await _invoiceLineService.GetAllbyInvoiceAsync(invoiceId);

        return PartialView("~/Views/InvoiceLines/_InvoiceLines.cshtml", invoiceLines);
    }

    public async Task<InvoiceLineModel> Save([FromBody] InvoiceLineModel invoiceLine)
    {
        var savedLined = await _invoiceLineService.SaveAsync(invoiceLine);

        return savedLined;
    }

    public async Task<bool> Delete(int id)
    {
        var isDeleted = await _invoiceLineService.DeleteAsync(id);

        return isDeleted;
    }
}
