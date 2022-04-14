using System;
using BlazoredRecaptcha.Interfaces;
using BlazoredRecaptcha.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace BlazoredRecaptcha;

public static class ExtensionMethods
{
    /// <summary>
    ///     Adds the library to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">The configuration you want to pass in</param>
    /// <returns></returns>
    public static IServiceCollection AddRecaptcha(this IServiceCollection services,
        Action<RecaptchaConfiguration> configuration)
    {
        _ = configuration ?? throw new ArgumentNullException(nameof(configuration));

        var config = new RecaptchaConfiguration();
        configuration(config);

        return services.AddScoped<IRecaptchaService>(x => new RecaptchaService(config, x.GetRequiredService<IJSRuntime>()));
    }
}