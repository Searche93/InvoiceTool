using InvoiceTool.Application.Models;

namespace InvoiceTool.Mvc.ViewModels.Settings;

public class EditSettingsViewModel
{
    public SettingsModel Settings { get; set; } = new SettingsModel();
    public IFormFile? CompanyLogoFile { get; set; }
}
    