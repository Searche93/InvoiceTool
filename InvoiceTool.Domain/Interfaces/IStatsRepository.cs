using InvoiceTool.Domain.Dto;

namespace InvoiceTool.Domain.Interfaces;

public interface IStatsRepository
{
    /// <summary>
    /// Retrieves the invoiced revenue amounts for each month within the current year.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see
    /// cref="MonthlyRevenueDto"/> objects, each representing the invoiced amount for a specific month. The list will be
    /// empty if no invoiced amounts are found for the year.</returns>
    Task<List<MonthlyRevenueDto>> GetYearlyInvoicedAmountStatic();
}
