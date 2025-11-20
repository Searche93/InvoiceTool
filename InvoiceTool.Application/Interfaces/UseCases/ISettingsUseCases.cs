using InvoiceTool.Application.UseCases.Settings;

namespace InvoiceTool.Application.Interfaces.UseCases;
public interface ISettingsUseCases
{
    IGetSettings GetSettings { get; }
    ISaveSettings SaveSettings { get; }
}
