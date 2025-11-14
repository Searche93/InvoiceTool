using AutoMapper;
using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Mapper;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;

namespace InvoiceTool.Application.Services;
public class SettingsService(ISettingsRepository settingsRepository) : ISettingsService
{
    private readonly IMapper Mapper = AutoMapperConfiguration.CreateMapper();
    private readonly ISettingsRepository _settingsRepository = settingsRepository;

    public async Task<SettingsModel?> GetSettingsAsync(Guid id)
    {
        var settings = await _settingsRepository.GetSettingsAsync(id);

        if (settings == null) return null;


        var settingsModel = Mapper.Map<SettingsModel?>(settings);

        return settingsModel;
    }

    public async Task<SettingsModel> SaveAsync(SettingsModel settingsModel)
    {
        var settings = Mapper.Map<Settings>(settingsModel);

        var savedSettings = await _settingsRepository.SaveAsync(settings);

        var savedSettingsModel = Mapper.Map<SettingsModel>(savedSettings);

        return savedSettingsModel;
    }
}
