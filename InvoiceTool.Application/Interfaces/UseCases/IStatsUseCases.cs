using InvoiceTool.Application.UseCases.Statistics;

namespace InvoiceTool.Application.Interfaces.UseCases;

public interface IStatsUseCases
{
    public IGetYearlyInvoicedAmountStatic GetYearlyInvoicedAmountStatic { get; }
}
