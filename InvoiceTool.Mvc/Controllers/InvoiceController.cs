using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.Customers;
using InvoiceTool.Application.UseCases.InvoiceLines;
using InvoiceTool.Application.UseCases.Invoices;
using InvoiceTool.Mvc.ViewModels.Invoice;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InvoiceTool.Mvc.Controllers;

public class InvoiceController(
    GetAllInvoices getAllInvoices,
    GetInvoiceById getInvoiceById,
    CreateInvoice createInvoice,
    EditInvoice editInvoice,
    GetInvoiceLinesByInvoiceId getInvoiceLinesByInvoiceId,
    DeleteInvoice deleteInvoice,

    GetAllCustomers getAllCustomers
) : Controller
{
    private readonly GetAllInvoices _getAllInvoices = getAllInvoices;
    private readonly GetInvoiceById _getInvoiceById = getInvoiceById;
    private readonly CreateInvoice _createInvoice = createInvoice;
    private readonly EditInvoice _editInvoice = editInvoice;
    private readonly GetInvoiceLinesByInvoiceId _getInvoiceLinesByInvoiceId = getInvoiceLinesByInvoiceId;
    private readonly DeleteInvoice _deleteInvoice = deleteInvoice;

    private readonly GetAllCustomers _getAllCustomers = getAllCustomers;

    public async Task<IActionResult> Index()
    {
        var invoices = await _getAllInvoices.Execute();

        var invoiceViewModel = new IndexInvoiceViewModel { Invoices = invoices };

        return View(invoiceViewModel);
    }

    public async Task<IActionResult> ViewInvoice(int id)
    {
        var invoice = await _getInvoiceById.Execute(id);

        var invoiceViewModel = new ViewInvoiceViewModel
        {
            Invoice = invoice
        };

        return View(invoiceViewModel);
    }

    public async Task<IActionResult> Create()
    {
        var model = new CreateInvoiceViewModel { Customers = await _getAllCustomers.Execute() };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Customers = await _getAllCustomers.Execute();

            return View(model.Invoice);
        }

        var invoiceLines = GenerateInvoiceLines();


        await _createInvoice.Execute(model.Invoice, invoiceLines);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var invoice = await _getInvoiceById.Execute(id);

        if (invoice == null)
        {
            return NotFound();
        }

        var viewModel = new EditInvoiceViewModel
        {
            Invoice = invoice,
            Customers = await _getAllCustomers.Execute()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditInvoiceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Customers = await _getAllCustomers.Execute();

            return View(model);
        }

        var invoiceLines = await _getInvoiceLinesByInvoiceId.Execute(model.Invoice.Id);

        await _editInvoice.Execute(model.Invoice, invoiceLines);

        return RedirectToAction("Index");
    }

    public async Task<bool> Delete(int id)
    {
        var isDeleted = await _deleteInvoice.Execute(id);

        return isDeleted;
    }


    private List<InvoiceLineModel> GenerateInvoiceLines()
    {
        var invoiceLinesJson = Request.Form.TryGetValue("InvoiceLinesJson", out var json) ? json.ToString() : string.Empty;

        return string.IsNullOrWhiteSpace(invoiceLinesJson)
            ? new List<InvoiceLineModel>()
            : JsonConvert.DeserializeObject<List<InvoiceLineModel>>(invoiceLinesJson) ?? new List<InvoiceLineModel>();
    }
}