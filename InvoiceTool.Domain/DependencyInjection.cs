using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace InvoiceTool.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.Scan(selector => selector.FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime()
        );

        return services;
    }
}
