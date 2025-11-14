using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Settings;
public class SaveSettings(ISettingsService settingsService)
{
    private readonly ISettingsService _settingsService = settingsService;

    public async Task<SettingsModel> Execute(SettingsModel settings)
    {
        if (settings == null)
            throw new ArgumentNullException(nameof(settings), "Settings cannot be null");

        var savedSettings = await _settingsService.SaveAsync(settings);

        return savedSettings;
    }
}
