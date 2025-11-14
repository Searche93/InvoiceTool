using InvoiceTool.Application.Models;
using InvoiceTool.Application.UseCases.Settings;
using InvoiceTool.Mvc.ViewModels.Settings;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;
public class SettingsController(GetSettings getSettings, SaveSettings saveSettings) : Controller
{
    private readonly GetSettings _getSettings = getSettings;
    private readonly SaveSettings _saveSettings = saveSettings;

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var settings = await _getSettings.Execute();

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
            model.Settings = await _getSettings.Execute() ?? new SettingsModel();

            return View(model);
        }

        await _saveSettings.Execute(model.Settings);

        TempData["SuccessMessage"] = "Instellingen succesvol opgeslagen!";

        return RedirectToAction("Edit");
    }
}
