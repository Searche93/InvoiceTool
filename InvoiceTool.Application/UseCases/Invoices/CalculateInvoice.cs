using AutoMapper;
using InvoiceTool.Application.Mapper;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.ValueObjects;

namespace InvoiceTool.Application.UseCases.Invoices;

public interface ICalculateInvoice
{
    InvoiceModel Execute(InvoiceModel invoice, List<InvoiceLineModel> invoiceLines);
}

public class CalculateInvoice : ICalculateInvoice
{
    private readonly IMapper Mapper = AutoMapperConfiguration.CreateMapper();
    public InvoiceModel Execute(InvoiceModel invoice, List<InvoiceLineModel> invoiceLines)
    {
        invoice.InvoiceLines = invoiceLines;

        var invoicesLinesEntity = Mapper.Map<List<InvoiceLine>>(invoice.InvoiceLines);

        invoice.NetPrice = InvoiceCalculations.CalculateNetPrice(invoicesLinesEntity);
        invoice.TaxPrice = InvoiceCalculations.CalculateTaxAmount(invoicesLinesEntity);
        invoice.GrossPrice = InvoiceCalculations.CalculateGrossPrice(invoicesLinesEntity);

        return invoice;
    }
}
