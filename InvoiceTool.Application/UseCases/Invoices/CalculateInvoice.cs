using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.ValueObjects;
using Mapster;

namespace InvoiceTool.Application.UseCases.Invoices;

public interface ICalculateInvoice
{
    InvoiceModel Execute(InvoiceModel invoice, List<InvoiceLineModel> invoiceLines);
}

public class CalculateInvoice : ICalculateInvoice
{
    public InvoiceModel Execute(InvoiceModel invoice, List<InvoiceLineModel> invoiceLines)
    {
        invoice.InvoiceLines = invoiceLines;

        var invoicesLinesEntity = invoice.InvoiceLines.Adapt<List<InvoiceLine>>();

        invoice.NetPrice = InvoiceCalculations.CalculateNetPrice(invoicesLinesEntity);
        invoice.TaxPrice = InvoiceCalculations.CalculateTaxAmount(invoicesLinesEntity);
        invoice.GrossPrice = InvoiceCalculations.CalculateGrossPrice(invoicesLinesEntity);

        return invoice;
    }
}
