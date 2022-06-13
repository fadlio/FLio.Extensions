using Microsoft.Extensions.Configuration;

namespace FLio.Extensions;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddJsonEmbedded(this IConfigurationBuilder builder, string path)
    {
        var assembly = typeof(ConfigurationBuilderExtensions).Assembly;
        var resourceNames = assembly.GetManifestResourceNames();
        var resourceName = resourceNames.FirstOrDefault(n => n.Contains(path));
        if (resourceName == null)
            throw new FileNotFoundException();

        var assemblyStream = assembly.GetManifestResourceStream(resourceName)!;
        builder.AddJsonStream(assemblyStream);
        return builder;
    }
}