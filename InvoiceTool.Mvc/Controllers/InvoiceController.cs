using InvoiceTool.Application.Interfaces.UseCases;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Enums;
using InvoiceTool.Mvc.Helpers;
using InvoiceTool.Mvc.ViewModels.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InvoiceTool.Mvc.Controllers;

[Authorize]
public class InvoiceController(
    IWebHostEnvironment env,
    IRazorViewToStringRenderer razorViewToStringRenderer,

    IInvoiceUseCases invoiceUseCases,
    IInvoiceLineUseCases invoiceLineUseCases,
    ICustomerUseCases customerUseCases,
    ISettingsUseCases settingsUseCases
) : Controller
{
    private readonly IWebHostEnvironment _env = env;
    private readonly IRazorViewToStringRenderer _razorViewToStringRenderer = razorViewToStringRenderer;

    private readonly IInvoiceUseCases _invoiceUseCases = invoiceUseCases;
    private readonly IInvoiceLineUseCases _invoiceLineUseCases = invoiceLineUseCases;
    private readonly ICustomerUseCases _customerUseCases = customerUseCases;
    private readonly ISettingsUseCases _settingsUseCases = settingsUseCases;

    public async Task<IActionResult> Index()
    {
        var invoices = await _invoiceUseCases.GetAllInvoices.ExecuteAsync();

        var invoiceViewModel = new IndexInvoiceViewModel { Invoices = invoices };

        return View(invoiceViewModel);
    }

    public async Task<IActionResult> ViewInvoice(int id)
    {
        var invoice = await _invoiceUseCases.GetInvoiceById.ExecuteAsync(id);

        var invoiceViewModel = new ViewInvoiceViewModel
        {
            Invoice = invoice
        };

        return View(invoiceViewModel);
    }

    public async Task<IActionResult> Create()
    {
        var model = new CreateInvoiceViewModel { Customers = await _customerUseCases.GetAllCustomers.ExecuteAsync() };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Customers = await _customerUseCases.GetAllCustomers.ExecuteAsync();

            return View(model.Invoice);
        }

        var invoiceLines = GenerateInvoiceLines();


        await _invoiceUseCases.CreateInvoice.ExecuteAsync(model.Invoice, invoiceLines);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var invoice = await _invoiceUseCases.GetInvoiceById.ExecuteAsync(id);

        if (invoice == null)
        {
            return NotFound();
        }

        var customers = await _customerUseCases.GetAllCustomers.ExecuteAsync();

        var viewModel = new EditInvoiceViewModel
        {
            Invoice = invoice,
            Customers = customers,
            PaymentStatusList = EnumHelper.GetSelectListFromEnum<PaymentStatus>()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditInvoiceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Customers = await _customerUseCases.GetAllCustomers.ExecuteAsync();

            model.PaymentStatusList = EnumHelper.GetSelectListFromEnum<PaymentStatus>();

            return View(model);
        }

        var invoiceLines = await _invoiceLineUseCases.GetInvoiceLinesByInvoiceId.ExecuteAsync(model.Invoice.Id);

        await _invoiceUseCases.EditInvoice.ExecuteAsync(model.Invoice, invoiceLines);

        return RedirectToAction("Index");
    }

    public async Task<bool> Delete(int id)
    {
        var isDeleted = await _invoiceUseCases.DeleteInvoice.ExecuteAsync(id);

        return isDeleted;
    }

    // Todo => Cleanup this method
    [HttpGet("DownloadPdf/{id}")]
    public async Task<FileResult> DownloadPdf(int id)
    {
#if HAS_DOCUMENTTOOLS

        if (id <= 0) throw new Exception("Invalid invoice id.");

        var invoice = await _invoiceUseCases.GetInvoiceById.ExecuteAsync(id) ?? throw new Exception("Invoice not found.");

        var customer = await _customerUseCases.GetCustomerById.ExecuteAsync(invoice.CustomerId) ?? throw new Exception("Customer not found.");

        var settings = await _settingsUseCases.GetSettings.ExecuteAsync() ?? new SettingsModel();

        if (settings != null && !string.IsNullOrEmpty(settings.CompanyLogo))
        {
            var logoPath = Path.Combine(_env.WebRootPath, "images", Path.GetFileName(settings.CompanyLogo));

            var logoBytes = await System.IO.File.ReadAllBytesAsync(logoPath);

            var base64Logo = Convert.ToBase64String(logoBytes);

            settings.CompanyLogo = $"data:image/png;base64,{base64Logo}";
        }

        var downloadPdfViewModel = new DownloadPdfViewModel
        {
            Invoice = invoice,
            Customer = customer,
            Settings = settings ?? new SettingsModel(),
        };

        var html = await _razorViewToStringRenderer.RenderViewToStringAsync(this, "DownloadPdf", downloadPdfViewModel);

        var bytes = _invoiceUseCases.CreatePdfByteArray.Execute(html);

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