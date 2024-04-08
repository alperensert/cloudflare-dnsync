using Spectre.Console.Cli;

namespace CloudflareDnsync.Cli.Commands;

public sealed class AddCommand : Command<AddCommand.Settings>
{
    public override int Execute(CommandContext contexti, Settings settings)
    {
        throw new System.NotImplementedException();
    }

    public sealed class Settings : ConfigurationCommand.ConfigurationSettings
    {
        [CommandOption("-t|--token <TOKEN>")]
        public string? Token { get; init; }
    }
}
