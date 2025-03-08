using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceTool.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}