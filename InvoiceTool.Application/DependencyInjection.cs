using InvoiceTool.Application.Interfaces;
using InvoiceTool.Application.Interfaces.UseCases;
using InvoiceTool.Application.Services;
using InvoiceTool.Application.UseCases.Customers;
using InvoiceTool.Application.UseCases.InvoiceLines;
using InvoiceTool.Application.UseCases.Invoices;
using InvoiceTool.Application.UseCases.Settings;
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
        services.AddScoped<ISettingsService, SettingsService>();

        // UseCases
        services.AddScoped<ICustomerUseCases, CustomerUseCases>();
        services.AddScoped<IGetAllCustomers, GetAllCustomers>();
        services.AddScoped<IGetAllCustomers, GetAllCustomers>();
        services.AddScoped<IGetCustomerById, GetCustomerById>();
        services.AddScoped<ICreateCustomer, CreateCustomer>();
        services.AddScoped<IEditCustomer, EditCustomer>();
        services.AddScoped<IDeleteCustomer, DeleteCustomer>();

        services.AddScoped<IInvoiceUseCases, InvoiceUseCases>();
        services.AddScoped<IGetAllInvoices, GetAllInvoices>();
        services.AddScoped<IGetInvoiceById, GetInvoiceById>();
        services.AddScoped<ICreateInvoice, CreateInvoice>();
        services.AddScoped<IEditInvoice, EditInvoice>();
        services.AddScoped<ICalculateInvoice, CalculateInvoice>();
        services.AddScoped<IGetInvoiceByInvoiceLineId, GetInvoiceByInvoiceLineId>();
        services.AddScoped<IDeleteInvoice, DeleteInvoice>();
        services.AddScoped<ICreatePdfByteArray, CreatePdfByteArray>();

        services.AddScoped<IInvoiceLineUseCases, InvoiceLineUseCases>();
        services.AddScoped<IGetInvoiceLinesByInvoiceId, GetInvoiceLinesByInvoiceId>();
        services.AddScoped<ISaveInvoiceLine, SaveInvoiceLine>();
        services.AddScoped<IDeleteInvoiceLine, DeleteInvoiceLine>();

        services.AddScoped<ISettingsUseCases, SettingsUseCases>();
        services.AddScoped<IGetSettings, GetSettings>();
        services.AddScoped<ISaveSettings, SaveSettings>();

        return services;
    }
}