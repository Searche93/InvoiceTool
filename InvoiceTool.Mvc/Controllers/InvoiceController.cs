using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Mvc.ViewModels.Invoice;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InvoiceTool.Mvc.Controllers;

public class InvoiceController(IInvoiceService invoiceService, ICustomerService customerService) : Controller
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    private readonly ICustomerService _customerService = customerService;

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

    public async Task<IActionResult> Create()
    {
        var model = new CreateInvoiceViewModel
        {
            Customers = await _customerService.GetAllAsync()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Customers = await _customerService.GetAllAsync();
            return View(model.Invoice);
        }

        model.Invoice.InvoiceLines = Request.Form.TryGetValue("InvoiceLinesJson", out var invoiceLinesJson) && !string.IsNullOrWhiteSpace(invoiceLinesJson)
            ? model.Invoice.InvoiceLines = JsonConvert.DeserializeObject<List<InvoiceLineModel>>(invoiceLinesJson) ?? new List<InvoiceLineModel>()
            : model.Invoice.InvoiceLines = new List<InvoiceLineModel>();


        model.Invoice.NetPrice = CalculateNetPrice(model.Invoice.InvoiceLines);
        model.Invoice.TaxPrice = CalculateTaxAmount(model.Invoice.InvoiceLines);
        model.Invoice.GrossPrice = CalculateGrossPrice(model.Invoice.InvoiceLines);

        await _invoiceService.SaveAsync(model.Invoice);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var invoice = await _invoiceService.GetAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }

        var viewModel = new EditInvoiceViewModel
        {
            Invoice = invoice,
            Customers = await _customerService.GetAllAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditInvoiceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Customers = await _customerService.GetAllAsync();

            return View(model);
        }

        // Todo
        // - Fix bug: if lines have not been added. Problem in JS.

        model.Invoice.InvoiceLines = Request.Form.TryGetValue("InvoiceLinesJson", out var invoiceLinesJson) && !string.IsNullOrWhiteSpace(invoiceLinesJson)
            ? model.Invoice.InvoiceLines = JsonConvert.DeserializeObject<List<InvoiceLineModel>>(invoiceLinesJson) ?? new List<InvoiceLineModel>()
            : model.Invoice.InvoiceLines = new List<InvoiceLineModel>();

        model.Invoice.NetPrice = CalculateNetPrice(model.Invoice.InvoiceLines);
        model.Invoice.TaxPrice = CalculateTaxAmount(model.Invoice.InvoiceLines);
        model.Invoice.GrossPrice = CalculateGrossPrice(model.Invoice.InvoiceLines);

        await _invoiceService.SaveAsync(model.Invoice);

        return RedirectToAction("Index");
    }






    // Todo make a global method somewhere in this project. DDD or in Application Layer?
    private static decimal CalculateNetPrice(List<InvoiceLineModel>? invoiceLines)
    {
        var netPrice = decimal.Zero;

        if (invoiceLines == null || invoiceLines.Count <= 0)
            return netPrice;

        foreach (var invoiceLine in invoiceLines)
        {
            netPrice += invoiceLine.UnitPrice * invoiceLine.Quantity;
        }

        return netPrice;
    }

    // Todo make a global method somewhere in this project. DDD or in Application Layer?
    private static decimal CalculateTaxAmount(List<InvoiceLineModel>? invoiceLines)
    {
        var taxAmount = decimal.Zero;

        if (invoiceLines == null || invoiceLines.Count <= 0)
            return taxAmount;

        foreach (var invoiceLine in invoiceLines)
        {
            var netPrice = invoiceLine.UnitPrice * invoiceLine.Quantity;

            taxAmount += (netPrice / 100) * invoiceLine.TaxPercentage;
        }

        return taxAmount;
    }

    // Todo make a global method somewhere in this project. DDD or in Application Layer?
    private static decimal CalculateGrossPrice(List<InvoiceLineModel>? invoiceLines)
    {
        var grossPrice = decimal.Zero;

        if (invoiceLines == null || invoiceLines.Count <= 0)
            return grossPrice;

        foreach (var invoiceLine in invoiceLines)
        {
            var netPrice = invoiceLine.UnitPrice * invoiceLine.Quantity;

            var taxAmount = (netPrice / 100) * invoiceLine.TaxPercentage;

            grossPrice += netPrice + taxAmount;
        }

        return grossPrice;
    }

}