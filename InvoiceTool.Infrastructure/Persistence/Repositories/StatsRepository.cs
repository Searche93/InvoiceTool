using InvoiceTool.Domain.Dto;
using InvoiceTool.Domain.Enums;
using InvoiceTool.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTool.Infrastructure.Persistence.Repositories;

internal class StatsRepository(AppDbContext context) : IStatsRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<MonthlyRevenueDto>> GetYearlyInvoicedAmountStatic()
    {
        var years = new int[] { DateTime.Today.Year - 2, DateTime.Today.Year - 1, DateTime.Today.Year };

        var monthlyTotals = await _context.Invoices
            .Where(i =>
                years.Contains(i.Date.Year) &&
                i.PaymentStatus == PaymentStatus.Completed
            )
            .GroupBy(i => new { i.Date.Year, i.Date.Month })
            .Select(g => new MonthlyRevenueDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Total = g.Sum(x => x.GrossPrice)
            })
            .ToListAsync();

        return monthlyTotals;
    }

    public async Task<decimal> TotalSalesByYear(int year)
    {
        var result = await _context.Invoices
            .Where(i => i.PaymentStatus == PaymentStatus.Completed && i.Date.Year == year)
            .SumAsync(i => i.GrossPrice);

        return result;
    }

    public async Task<decimal> TotalSalesByMonth(int month)
    {
        var result = await _context.Invoices
            .Where(i => 
                i.PaymentStatus == PaymentStatus.Completed && 
                i.Date.Year == DateTime.Now.Year &&
                i.Date.Month == month
            )
            .SumAsync(i => i.GrossPrice);

        return result;
    }

    public async Task<int> TotalPendingInvoices()
    {
        var result = await _context.Invoices
            .Where(i => i.PaymentStatus == PaymentStatus.Pending)
            .CountAsync();

        return result;
    }
}
