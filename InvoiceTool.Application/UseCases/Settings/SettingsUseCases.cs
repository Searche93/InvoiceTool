using InvoiceTool.Application.Interfaces.UseCases;

namespace InvoiceTool.Application.UseCases.Settings;

public class SettingsUseCases(
    IGetSettings getSettings,
    ISaveSettings saveSettings) : ISettingsUseCases
{
    public IGetSettings GetSettings { get; } = getSettings;
    public ISaveSettings SaveSettings { get; } = saveSettings;
}
