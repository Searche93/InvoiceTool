using InvoiceTool.Domain.Dto;
using InvoiceTool.Domain.Enums;

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

    /// <summary>
    /// Asynchronously calculates the total sales amount for the specified year.
    /// </summary>
    /// <param name="year">The year for which to calculate total sales. Must be a four-digit year greater than 0.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the total sales amount for the
    /// specified year as a decimal value.</returns>
    Task<decimal> TotalSalesByYear(int year);

    /// <summary>
    /// Asynchronously calculates the total sales amount for the specified month.
    /// </summary>
    /// <param name="month">The month for which to calculate total sales. Must be an integer from 1 (January) to 12 (December).</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the total sales amount for the
    /// specified month as a decimal value.</returns>
    Task<decimal> TotalSalesByMonth(int month);

    /// <summary>
    /// Asynchronously retrieves the total number of invoices that are pending payment.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of pending invoices.
    /// Returns 0 if there are no pending invoices.</returns>
    Task<int> TotalPendingInvoices();
}
