using InvoiceTool.Application.Interfaces.UseCases;
using InvoiceTool.Application.Models;
using InvoiceTool.Mvc.ViewModels.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTool.Mvc.Controllers;

[Authorize]
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

        if (model.CompanyLogoFile != null && model.CompanyLogoFile.Length > 0)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.CompanyLogoFile.FileName)}";

            var fullPath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await model.CompanyLogoFile.CopyToAsync(stream);
            }

            model.Settings.CompanyLogo = $"/images/{fileName}";
        }

        await _settingsUseCases.SaveSettings.ExecuteAsync(model.Settings);

        TempData["SuccessMessage"] = "Instellingen succesvol opgeslagen!";

        return RedirectToAction("Edit");
    }
}
