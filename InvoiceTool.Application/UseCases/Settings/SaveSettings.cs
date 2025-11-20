using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Settings;

public interface ISaveSettings
{
    Task<SettingsModel> ExecuteAsync(SettingsModel settings);
}

public class SaveSettings(ISettingsService settingsService) : ISaveSettings
{
    private readonly ISettingsService _settingsService = settingsService;

    public async Task<SettingsModel> ExecuteAsync(SettingsModel settings)
    {
        if (settings == null)
            throw new ArgumentNullException(nameof(settings), "Settings cannot be null");

        var savedSettings = await _settingsService.SaveAsync(settings);

        return savedSettings;
    }
}
