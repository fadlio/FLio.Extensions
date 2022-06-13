using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FLio.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddOptionsWithValidation<T>(
        this IServiceCollection services,
        HostBuilderContext builder
    ) where T : class
    {
        services
            .AddOptions<T>()
            .Bind(builder.Configuration.GetSection(typeof(T).Name[..^7]))
            .ValidateDataAnnotations();

        // Explicitly register the settings object by delegating to the IOptions object and performs initial validation
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<T>>().Value);
    }
}