using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.UseCases.Settings;

public interface IGetSettings
{
    Task<SettingsModel?> ExecuteAsync();
}

public class GetSettings(ISettingsService settingsService) : IGetSettings
{
    private readonly ISettingsService _settingsService = settingsService;

    public async Task<SettingsModel?> ExecuteAsync()
    {
        // Temp hardcoded. In the future I might add multiple companies wich are connected to settings. 
        var id = new Guid("b9c6ed20-13e0-4bb1-b074-556d9fae7f19");

        var settings = await _settingsService.GetSettingsAsync(id);

        return settings;
    }
}
