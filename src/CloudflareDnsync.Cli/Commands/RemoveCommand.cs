using CloudflareDnsync.Cli.Extensions;
using CloudflareDnsync.Models;
using CloudflareDnsync.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CloudflareDnsync.Cli.Commands;

public sealed class RemoveCommand(ILogger<RemoveCommand> logger, IDnsyncConfigService configService) : AsyncCommand<RemoveCommand.Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var configurations = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Default)
            .StartAsync("Retrieving configurations...", async ctx =>
            {
                var configurations = configService.GetAll();
                await Task.Delay(100);
                if (!configurations.Any())
                {
                    logger.LogInformation("No configurations found.");
                    return null;
                }
                return configurations;
            });
        if (configurations is null)
            return 0;
        var configuration = AnsiConsole.Prompt(new SelectionPrompt<DnsyncConfiguration>()
                .Title("Select a configuration to remove:".ToTimestampedString())
                .PageSize(10)
                .MoreChoicesText("More")
                .AddChoices(configurations)
                .UseConverter(c => c.Name));
        if (configuration is null)
            return 0;
        var confirm = AnsiConsole.Confirm("Are you sure you want to remove this configuration?".ToTimestampedString());
        if (!confirm)
        {
            logger.LogInformation("Operation cancelled.");
            return 0;
        }
        try
        {
            await configService.RemoveAsync(configuration);
            logger.LogInformation("Configuration removed.");
            return 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to remove configuration.");
            return 1;
        }
    }

    public sealed class Settings : ConfigurationCommand.ConfigurationSettings
    {
    }
}
