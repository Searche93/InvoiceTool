using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Services;
using InvoiceTool.Application.UseCases.Customers;
using InvoiceTool.Application.UseCases.InvoiceLines;
using InvoiceTool.Application.UseCases.Invoices;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceTool.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Todo cleanup DI

        // Services
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IInvoiceLineService, InvoiceLineService>();
        services.AddScoped<ICustomerService, CustomerService>();

        // UseCases
        services.AddScoped<GetAllCustomers>();
        services.AddScoped<GetCustomerById>();
        services.AddScoped<CreateCustomer>();
        services.AddScoped<EditCustomer>();

        services.AddScoped<GetAllInvoices>();
        services.AddScoped<GetInvoiceById>();
        services.AddScoped<CreateInvoice>();
        services.AddScoped<EditInvoice>();
        services.AddScoped<CalculateInvoice>();

        services.AddScoped<GetInvoiceLinesByInvoiceId>();
        services.AddScoped<SaveInvoiceLine>();
        services.AddScoped<DeleteInvoiceLine>();

        return services;
    }
}