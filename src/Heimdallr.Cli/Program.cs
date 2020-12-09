using System;
using System.Globalization;
using System.Threading;
using CommandLine;
using Heimdallr.Security.Scrypt;

namespace Heimdallr.Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Configuration>(args)
                .WithParsed(Run);
        }

        public static void Run(Configuration config)
        {
            var container = ProgramExtensions.ConfigureApp(services => services.AddScryptModule());
            var passwordService = container.GetPasswordService();

            var securityRequest = config.ToSecurityRequest();

            securityRequest.Validate();

            var password = passwordService.Generate(securityRequest)
                .GetAwaiter()
                .GetResult();

            Console.WriteLine(password.GeneratedPassword);
        }
    }
}