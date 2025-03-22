using InvoiceTool.Domain.Entities;

namespace InvoiceTool.Domain.ValueObjects;

public static class InvoiceCalculations
{
    public static decimal CalculateNetPrice(List<InvoiceLine> invoiceLines)
    {
        if(invoiceLines.Count <= 0)
            return decimal.Zero;

        return invoiceLines.Sum(line => line.UnitPrice * line.Quantity);
    }

    public static decimal CalculateTaxAmount(List<InvoiceLine> invoiceLines)
    {
        if (invoiceLines.Count <= 0)
            return decimal.Zero;

        return invoiceLines.Sum(line => (line.UnitPrice * line.Quantity / 100) * line.TaxPercentage);   
    }

    public static decimal CalculateGrossPrice(List<InvoiceLine> invoiceLines)
    {
        if (invoiceLines.Count <= 0)
            return decimal.Zero;

        return CalculateNetPrice(invoiceLines) + CalculateTaxAmount(invoiceLines);
    }
}
