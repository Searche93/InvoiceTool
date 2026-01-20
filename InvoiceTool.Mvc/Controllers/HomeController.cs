using InvoiceTool.Application.Interfaces.UseCases;
using InvoiceTool.Mvc.Models;
using InvoiceTool.Mvc.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InvoiceTool.Mvc.Controllers;

[Authorize]
public class HomeController(IStatsUseCases statsUseCases) : Controller
{
    private readonly IStatsUseCases _statsUseCases = statsUseCases;

    public async Task<IActionResult> Index()
    {
        var totalSalesThisYear = await _statsUseCases.TotalSalesByYear.Execute(DateTime.Now.Year);
        var totalSalesThisMonth = await _statsUseCases.TotalSalesByMonth.Execute(DateTime.Now.Month);
        var totalPendingInvoices = await _statsUseCases.TotalPendingInvoices.Execute();
        var monthlyRevenue = await _statsUseCases.GetYearlyInvoicedAmountStatic.Execute();

        var model = new DashboardViewModel
        {
            TotalSalesThisYear = totalSalesThisYear,
            TotalSalesThisMonth = totalSalesThisMonth,
            TotalPendingInvoices = totalPendingInvoices,
            MonthlyRevenueResult = monthlyRevenue
        };

        return View(model);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
