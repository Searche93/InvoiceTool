using InvoiceTool.Application.Interfaces.UseCases;
using InvoiceTool.Mvc.Models;
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
        var model = await _statsUseCases.GetYearlyInvoicedAmountStatic.Execute();

        return View(model);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
