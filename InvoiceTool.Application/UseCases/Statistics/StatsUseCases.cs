using InvoiceTool.Application.Interfaces.UseCases;

namespace InvoiceTool.Application.UseCases.Statistics;

public class StatsUseCases(
    IGetYearlyInvoicedAmountStatic getYearlyInvoicedAmountStatic,
    ITotalSalesByYear totalSalesByYear,
    ITotalSalesByMonth totalSalesByMonth,
    ITotalPendingInvoices totalPendingInvoices) : IStatsUseCases
{
    public IGetYearlyInvoicedAmountStatic GetYearlyInvoicedAmountStatic { get; } = getYearlyInvoicedAmountStatic;
    public ITotalSalesByYear TotalSalesByYear { get; } = totalSalesByYear;
    public ITotalSalesByMonth TotalSalesByMonth { get; } = totalSalesByMonth;
    public ITotalPendingInvoices TotalPendingInvoices { get; } = totalPendingInvoices;
}
