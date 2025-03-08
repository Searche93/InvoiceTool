using AutoMapper;
using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Mapper;
using InvoiceTool.Application.Models;
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

    public async Task<List<InvoiceModel>> GetAllAsync()
    {
        var invoices = await _invoiceRepository.GetAllAsync();

        if (invoices == null) return new List<InvoiceModel>();


        var listOfInvoices = Mapper.Map<List<InvoiceModel>>(invoices);

        return listOfInvoices ?? new List<InvoiceModel>();
    }
}