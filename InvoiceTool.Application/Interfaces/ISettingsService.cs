using InvoiceTool.Application.Models;

namespace InvoiceTool.Application.Interfaces;
public interface ISettingsService
{
    /// <summary>
    /// Get the settings
    /// </summary>
    /// /// <param name="id"></param>
    /// <returns>Settings object</returns>
    Task<SettingsModel?> GetSettingsAsync(Guid id);

    /// <summary>
    /// Save settings
    /// </summary>
    /// <param name="settings"></param>
    /// <returns>Settings object</returns>
    Task<SettingsModel> SaveAsync(SettingsModel settings);
}
