using CommandLine;

namespace Heimdallr.Cli;

/// <summary>
/// Heimdallr CLI password generate configuration
/// </summary>
public class Configuration
{
    [Option('s', "service",
        Required = true,
        HelpText = "Service name for which the password is generated")]
    public string ServiceName { get; set; }

    [Option('n',"name",
        Required = true,
        HelpText = "Service account or password ID")]
    public string CommonName { get; set; }

    [Option('p',"mpwd",
        HelpText = "Master password to generate an idempotent password based on the service name and common name and the dictionary used")]
    public string MasterPassword { get; set; }

    [Option(Default = true,
        HelpText = "Numeric dictionary for password generation")]
    public bool? HasNumeric { get; set; }

    [Option(Default = true, 
        HelpText = "Letter dictionary for password generation")]
    public bool? HasLetters { get; set; }

    [Option(Default = true,
        HelpText = "Symbolic dictionary for password generation")]
    public bool? HasSpecialSymbols { get; set; }

    [Option('l', Default = 16,
        HelpText = "Length of generated password")]
    public int Length { get; set; }

    [Option('v', Default = 1,
        HelpText = "Password version (if you need a new password for the same service and account)")]
    public int Version { get; set; }
}