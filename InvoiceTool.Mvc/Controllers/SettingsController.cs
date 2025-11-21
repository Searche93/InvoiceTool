using InvoiceTool.Application.Interfaces.UseCases;
using InvoiceTool.Application.Models;
using InvoiceTool.Mvc.ViewModels.Settings;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;
public class SettingsController(ISettingsUseCases settingsUseCases) : Controller
{
    private readonly ISettingsUseCases _settingsUseCases = settingsUseCases;

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var settings = await _settingsUseCases.GetSettings.ExecuteAsync();

        if (settings == null)
        {
            return NotFound();
        }

        var viewModel = new EditSettingsViewModel
        {
            Settings = settings
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditSettingsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Settings = await _settingsUseCases.GetSettings.ExecuteAsync() ?? new SettingsModel();

            return View(model);
        }

        await _settingsUseCases.SaveSettings.ExecuteAsync(model.Settings);

        TempData["SuccessMessage"] = "Instellingen succesvol opgeslagen!";

        return RedirectToAction("Edit");
    }
}
