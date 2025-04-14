using Heimdallr.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace Heimdallr.Cli;

public static class ProgramExtensions
{
    public static IServiceProvider ConfigureApp(Action<IServiceCollection> buildAction)
    {
        var collection = new ServiceCollection();
        buildAction?.Invoke(collection);
        return collection.BuildServiceProvider().CreateScope().ServiceProvider;
    }

    public static IPasswordService GetPasswordService(this IServiceProvider provider)
    {
        return provider.GetService<IPasswordService>();
    }

    public static SecurityRequest ToSecurityRequest(this Configuration config)
    {
        return new SecurityRequest
        {
            ServiceName = config.ServiceName,
            CommonName = config.CommonName,
            HasLetters = config?.HasLetters ?? true,
            HasNumeric = config?.HasNumeric ?? true,
            HasSpecialSymbols = config?.HasSpecialSymbols ?? true,
            MasterPassword = config.MasterPassword,
            Length = config.Length,
            Version = config.Version
        };
    }

    public static void Validate(this object validateObject)
    {
        if (!(validateObject is IValidatableObject)) return;

        var ctx = new ValidationContext(validateObject);
        Validator.ValidateObject(validateObject, ctx, true);
    }

}