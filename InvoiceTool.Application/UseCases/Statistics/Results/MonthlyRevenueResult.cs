namespace InvoiceTool.Application.UseCases.Statistics.Results;

public class MonthlyRevenueResult
{
    public List<string> Labels { get; set; } = new();
    public Dictionary<int, List<decimal>> YearValues { get; set; } = new();
}

