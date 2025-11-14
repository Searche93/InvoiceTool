using InvoiceTool.Domain.Entities;

namespace InvoiceTool.Domain.Interfaces;
public interface ISettingsRepository
{
    /// <summary>
    /// Get the settings
    /// </summary>
    /// /// <param name="id"></param>
    /// <returns>Settings object</returns>
    Task<Settings?> GetSettingsAsync(Guid id);

    /// <summary>
    /// Save settings
    /// </summary>
    /// <param name="settings"></param>
    /// <returns>Settings object</returns>
    Task<Settings> SaveAsync(Settings settings); 
}
