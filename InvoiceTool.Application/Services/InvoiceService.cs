using AutoMapper;
using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Mapper;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;

namespace InvoiceTool.Application.Services;

internal class InvoiceService(IInvoiceRepository invoiceRepository) : IInvoiceService
{
    private readonly IMapper Mapper = AutoMapperConfiguration.CreateMapper();
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

    public async Task<InvoiceModel?> GetAsync(int id)
    {
        var invoice = await _invoiceRepository.GetAsync(id);

        if (invoice == null) return null;


        var invoiceModel = Mapper.Map<InvoiceModel?>(invoice);

        return invoiceModel;
    }

    public async Task<InvoiceModel?> GetInvoiceByInvoiceLineIdAsync(int invoiceLineId)
    {
        var invoice = await _invoiceRepository.GetInvoiceByInvoiceLineIdAsync(invoiceLineId);

        if (invoice == null) return null;


        var invoiceModel = Mapper.Map<InvoiceModel?>(invoice);

        return invoiceModel;
    }

    public async Task<List<InvoiceModel>> GetAllAsync()
    {
        var invoices = await _invoiceRepository.GetAllAsync();

        if (invoices == null) return new List<InvoiceModel>();


        var listOfInvoices = Mapper.Map<List<InvoiceModel>>(invoices);

        return listOfInvoices ?? new List<InvoiceModel>();
    }

    public async Task<InvoiceModel> SaveAsync(InvoiceModel invoiceModel)
    {
        var invoice = Mapper.Map<Invoice>(invoiceModel);

        var savedInvoice = await _invoiceRepository.SaveAsync(invoice);

        var savedInvoiceModel = Mapper.Map<InvoiceModel>(savedInvoice);

        return savedInvoiceModel;
    }

    public async Task<bool> DeleteAsync(int invoiceId)
    {
        return await _invoiceRepository.DeleteAsync(invoiceId);
    }
}