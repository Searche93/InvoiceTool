using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;
using Mapster;

namespace InvoiceTool.Application.Services;

internal class InvoiceLineService(IInvoiceLineRepository invoiceLineRepository) : IInvoiceLineService
{
    private readonly IInvoiceLineRepository _invoiceLineRepository = invoiceLineRepository;

    public async Task<List<InvoiceLineModel>?> GetAllbyInvoiceAsync(int invoiceId)
    {
        var invoiceLines = await _invoiceLineRepository.GetAllbyInvoiceAsync(invoiceId);

        if (invoiceLines == null) return null;


        var invoiceLinesModel = invoiceLines.Adapt<List<InvoiceLineModel>>();

        return invoiceLinesModel ?? new List<InvoiceLineModel>();
    }

    public async Task<InvoiceLineModel> SaveAsync(InvoiceLineModel invoiceLineModel)
    {
        var invoiceLine = invoiceLineModel.Adapt<InvoiceLine>();

        var savedInvoiceLine = await _invoiceLineRepository.SaveAsync(invoiceLine);

        var savedInvoiceLineModel = savedInvoiceLine.Adapt<InvoiceLineModel>();

        return savedInvoiceLineModel;
    }

    public async Task<bool> DeleteAsync(int invoiceLineId)
    {
        var isDeleted = await _invoiceLineRepository.DeleteAsync(invoiceLineId);

        return isDeleted;
    }
}
