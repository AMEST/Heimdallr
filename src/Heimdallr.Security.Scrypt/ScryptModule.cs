using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("Heimdallr.Securyty.Scrypt.Tests")]
namespace Heimdallr.Security.Scrypt
{
    public static class ScryptModule
    {
        public static IServiceCollection AddScryptModule(this IServiceCollection collection)
        {
            collection.AddScoped<IPasswordService, PasswordService>();
            collection.AddTransient<PasswordHashGenerator>();
            return collection;
        }
    }
}