using InvoiceTool.Application.Interfaces;
using InvoiceTool.Domain.Interfaces;
using InvoiceTool.Infrastructure.Files;
using InvoiceTool.Infrastructure.Persistence;
using InvoiceTool.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceTool.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceLineRepository, InvoiceLineRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IPdfGenerator, PdfGenerator>();

        return services;
    }
}