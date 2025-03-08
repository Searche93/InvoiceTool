using InvoiceTool.Application.Interfaces;
using InvoiceTool.Mvc.ViewModels.Invoice;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;

public class InvoiceController(IInvoiceService invoiceService) : Controller
{
    private readonly IInvoiceService _invoiceService = invoiceService;


    public async Task<IActionResult> Index()
    {
        var invoices = await _invoiceService.GetAllAsync();

        var invoiceViewModel = new IndexInvoiceViewModel
        {
            Invoices = invoices
        };

        return View(invoiceViewModel);
    }

    public async Task<IActionResult> ViewInvoice(int id)
    {
        var invoice = await _invoiceService.GetAsync(id);

        var invoiceViewModel = new ViewInvoiceViewModel
        {
            Invoice = invoice
        };

        return View(invoiceViewModel);
    }
}
