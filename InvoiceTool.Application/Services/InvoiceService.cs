using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;
using Mapster;

namespace InvoiceTool.Application.Services;

internal class InvoiceService(IInvoiceRepository invoiceRepository) : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

    public async Task<InvoiceModel?> GetAsync(int id)
    {
        var invoice = await _invoiceRepository.GetAsync(id);

        if (invoice == null) return null;


        var invoiceModel = invoice.Adapt<InvoiceModel?>();

        return invoiceModel;
    }

    public async Task<InvoiceModel?> GetInvoiceByInvoiceLineIdAsync(int invoiceLineId)
    {
        var invoice = await _invoiceRepository.GetInvoiceByInvoiceLineIdAsync(invoiceLineId);

        if (invoice == null) return null;


        var invoiceModel = invoice.Adapt<InvoiceModel?>();

        return invoiceModel;
    }

    public async Task<List<InvoiceModel>> GetAllAsync()
    {
        var invoices = await _invoiceRepository.GetAllAsync();

        if (invoices == null) return new List<InvoiceModel>();


        var listOfInvoices = invoices.Adapt<List<InvoiceModel>>();

        return listOfInvoices ?? new List<InvoiceModel>();
    }

    public async Task<InvoiceModel> SaveAsync(InvoiceModel invoiceModel)
    {
        var invoice = invoiceModel.Adapt<Invoice>();

        var savedInvoice = await _invoiceRepository.SaveAsync(invoice);

        var savedInvoiceModel = savedInvoice.Adapt<InvoiceModel>();

        return savedInvoiceModel;
    }

    public async Task<bool> DeleteAsync(int invoiceId)
    {
        return await _invoiceRepository.DeleteAsync(invoiceId);
    }

    public async Task<List<InvoiceModel>> SearchAsync(string searchInput)
    {
        var invoices = await _invoiceRepository.SearchAsync(searchInput);
        
        if (invoices == null) return new List<InvoiceModel>();
        
        var listOfInvoices = invoices.Adapt<List<InvoiceModel>>();
     
        return listOfInvoices ?? new List<InvoiceModel>();
    }
}