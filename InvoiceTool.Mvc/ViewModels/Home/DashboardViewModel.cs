using InvoiceTool.Application.UseCases.Statistics.Results;

namespace InvoiceTool.Mvc.ViewModels.Home;

public class DashboardViewModel
{
    public decimal TotalSalesThisYear { get; set; }
    public decimal TotalSalesThisMonth { get; set; }
    public int TotalPendingInvoices { get; set; }
    public MonthlyRevenueResult MonthlyRevenueResult { get; set; } = new MonthlyRevenueResult();
}
