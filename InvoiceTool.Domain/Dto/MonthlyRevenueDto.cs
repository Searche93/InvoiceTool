namespace InvoiceTool.Domain.Dto;

public class MonthlyRevenueDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Total { get; set; }
}
