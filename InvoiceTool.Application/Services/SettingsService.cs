using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;
using Mapster;

namespace InvoiceTool.Application.Services;
public class SettingsService(ISettingsRepository settingsRepository) : ISettingsService
{
    private readonly ISettingsRepository _settingsRepository = settingsRepository;

    public async Task<SettingsModel?> GetSettingsAsync(Guid id)
    {
        var settings = await _settingsRepository.GetSettingsAsync(id);

        if (settings == null) return null;


        var settingsModel = settings.Adapt<SettingsModel>();

        return settingsModel;
    }

    public async Task<SettingsModel> SaveAsync(SettingsModel settingsModel)
    {
        var settings = settingsModel.Adapt<Settings>();

        var savedSettings = await _settingsRepository.SaveAsync(settings);

        var savedSettingsModel = savedSettings.Adapt<SettingsModel>();

        return savedSettingsModel;
    }
}
