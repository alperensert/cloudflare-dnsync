using Spectre.Console.Cli;

namespace CloudflareDnsync.Cli.Commands;

public sealed class ConfigurationCommand
{
    // This is a command that has subcommands.
    // Spectre.Console.Cli will automatically show the help for this command
    // and its subcommands when the user runs the command without any arguments.

    public class ConfigurationSettings : CommandSettings
    {
    }
}
