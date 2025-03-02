using InvoiceTool.Application.Interfaces;
using InvoiceTool.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;

public class InvoiceController(IInvoiceService invoiceService) : Controller
{
    private readonly IInvoiceService _invoiceService = invoiceService;


    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> ViewInvoice(int id)
    {
        var invoice = await _invoiceService.Get(id);

        var invoiceViewModel = new ViewInvoiceViewModel
        {
            Invoice = invoice
        };

        return View(invoiceViewModel);
    }
}
