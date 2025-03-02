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

    public async Task<InvoiceModel?> Get(int id)
    {
        var invoice = await _invoiceRepository.Get(id);

        if (invoice == null) return null;


        var invoiceModel = Mapper.Map<InvoiceModel?>(invoice);

        return invoiceModel;
    }

    public Task<List<InvoiceModel>> GetAll()
    {
        throw new NotImplementedException();
    }
}