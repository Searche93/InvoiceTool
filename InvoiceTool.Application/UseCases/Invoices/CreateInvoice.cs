using AutoMapper;
using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Mapper;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.ValueObjects;

namespace InvoiceTool.Application.UseCases.Invoices;

public class CreateInvoice(IInvoiceService invoiceService)
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    private readonly IMapper Mapper = AutoMapperConfiguration.CreateMapper();

    public async Task Execute(InvoiceModel invoice, List<InvoiceLineModel> invoiceLines)
    {
        invoice.InvoiceLines = invoiceLines;

        var invoicesLinesEntity = Mapper.Map<List<InvoiceLine>>(invoice.InvoiceLines);

        invoice.NetPrice = InvoiceCalculations.CalculateNetPrice(invoicesLinesEntity);
        invoice.TaxPrice = InvoiceCalculations.CalculateTaxAmount(invoicesLinesEntity);
        invoice.GrossPrice = InvoiceCalculations.CalculateGrossPrice(invoicesLinesEntity);

        await _invoiceService.SaveAsync(invoice);
    }
}
