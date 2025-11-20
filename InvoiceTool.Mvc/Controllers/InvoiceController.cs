using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.Customers;
using InvoiceTool.Application.UseCases.InvoiceLines;
using InvoiceTool.Application.UseCases.Invoices;
using InvoiceTool.Application.UseCases.Settings;
using InvoiceTool.Mvc.Helpers;
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
    CreatePdfByteArray createPdfByteArray,

    GetAllCustomers getAllCustomers,
    GetCustomerById getCustomerById,
    GetSettings getSettings,
    IRazorViewToStringRenderer razorViewToStringRenderer
) : Controller
{
    private readonly IRazorViewToStringRenderer _razorViewToStringRenderer = razorViewToStringRenderer;

    private readonly GetAllInvoices _getAllInvoices = getAllInvoices;
    private readonly GetInvoiceById _getInvoiceById = getInvoiceById;
    private readonly CreateInvoice _createInvoice = createInvoice;
    private readonly EditInvoice _editInvoice = editInvoice;
    private readonly GetInvoiceLinesByInvoiceId _getInvoiceLinesByInvoiceId = getInvoiceLinesByInvoiceId;
    private readonly DeleteInvoice _deleteInvoice = deleteInvoice;

    private readonly CreatePdfByteArray _createPdfByteArray = createPdfByteArray;

    private readonly GetAllCustomers _getAllCustomers = getAllCustomers;
    private readonly GetCustomerById _getCustomerById = getCustomerById;
    private readonly GetSettings _getSettings = getSettings;

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
        var model = new CreateInvoiceViewModel { Customers = await _getAllCustomers.ExecuteAsync() };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Customers = await _getAllCustomers.ExecuteAsync();

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
            Customers = await _getAllCustomers.ExecuteAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditInvoiceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Customers = await _getAllCustomers.ExecuteAsync();

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

    // Todo => Cleanup this method
    [HttpGet("DownloadPdf/{id}")]
    public async Task<FileResult> DownloadPdf(int id)
    {
#if HAS_DOCUMENTTOOLS

        if (id <= 0) throw new Exception("Invalid invoice id.");

        var invoice = await _getInvoiceById.Execute(id) ?? throw new Exception("Invoice not found.");

        var customer = await _getCustomerById.ExecuteAsync(invoice.CustomerId) ?? throw new Exception("Customer not found.");

        var downloadPdfViewModel = new DownloadPdfViewModel
        {
            Invoice = invoice,
            Customer = customer,
            Settings = await _getSettings.Execute() ?? new SettingsModel(),
        };

        var html = await _razorViewToStringRenderer.RenderViewToStringAsync(this, "DownloadPdf", downloadPdfViewModel);

        var bytes = _createPdfByteArray.Execute(html);

        return File(bytes, "application/pdf", $"invoice-{invoice.Number}.pdf");

#else
        var message = "PDF generation is only available for users with acces to the DocumentTools package.";
        
        var fallbackBytes = System.Text.Encoding.UTF8.GetBytes($"<html><body><h1>{message}</h1></body></html>");
            
        return File(fallbackBytes, "text/html", "not-available.html");
#endif
    }


    private List<InvoiceLineModel> GenerateInvoiceLines()
    {
        var invoiceLinesJson = Request.Form.TryGetValue("InvoiceLinesJson", out var json) ? json.ToString() : string.Empty;

        return string.IsNullOrWhiteSpace(invoiceLinesJson)
            ? new List<InvoiceLineModel>()
            : JsonConvert.DeserializeObject<List<InvoiceLineModel>>(invoiceLinesJson) ?? new List<InvoiceLineModel>();
    }
}