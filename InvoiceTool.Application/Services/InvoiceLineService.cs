using AutoMapper;
using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Mapper;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;

namespace InvoiceTool.Application.Services;

internal class InvoiceLineService(IInvoiceLineRepository invoiceLineRepository) : IInvoiceLineService
{
    private readonly IMapper Mapper = AutoMapperConfiguration.CreateMapper();
    private readonly IInvoiceLineRepository _invoiceLineRepository = invoiceLineRepository;

    public async Task<List<InvoiceLineModel>?> GetAllbyInvoiceAsync(int invoiceId)
    {
        List<InvoiceLine> invoiceLines = await _invoiceLineRepository.GetAllbyInvoiceAsync(invoiceId);

        if (invoiceLines == null) return null;


        List<InvoiceLineModel>? invoiceLinesModel = Mapper.Map<List<InvoiceLineModel>?>(invoiceLines);

        return invoiceLinesModel ?? new List<InvoiceLineModel>();
    }

    public async Task<InvoiceLineModel> SaveAsync(InvoiceLineModel invoiceLineModel)
    {
        InvoiceLine invoiceLine = Mapper.Map<InvoiceLine>(invoiceLineModel);

        InvoiceLine savedInvoiceLine = await _invoiceLineRepository.SaveAsync(invoiceLine);

        InvoiceLineModel savedInvoiceLineModel = Mapper.Map<InvoiceLineModel>(savedInvoiceLine);

        return savedInvoiceLineModel;
    }

    public async Task<bool> DeleteAsync(int invoiceLineId)
    {
        bool isDeleted = await _invoiceLineRepository.DeleteAsync(invoiceLineId);

        return isDeleted;
    }
}
