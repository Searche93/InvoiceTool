namespace InvoiceTool.Mvc.ViewModels.Statistics;

public class MonthlyRevenueViewModel
{
    public List<string> Months { get; set; } = new();
    public List<decimal> Totals { get; set; } = new();
}
