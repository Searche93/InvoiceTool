using InvoiceTool.Application.UseCases.Statistics;

namespace InvoiceTool.Application.Interfaces.UseCases;

public interface IStatsUseCases
{
    public IGetYearlyInvoicedAmountStatic GetYearlyInvoicedAmountStatic { get; }
    public ITotalSalesByYear TotalSalesByYear { get; }
    public ITotalSalesByMonth TotalSalesByMonth { get; }
    public ITotalPendingInvoices TotalPendingInvoices { get; }
}
