using InvoiceTool.Application.Interfaces.UseCases;

namespace InvoiceTool.Application.UseCases.Statistics;

public class StatsUseCases(
    IGetYearlyInvoicedAmountStatic getYearlyInvoicedAmountStatic) : IStatsUseCases
{
    public IGetYearlyInvoicedAmountStatic GetYearlyInvoicedAmountStatic { get; } = getYearlyInvoicedAmountStatic;
}
