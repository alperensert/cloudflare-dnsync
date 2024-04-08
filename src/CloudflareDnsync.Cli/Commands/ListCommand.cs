using System.ComponentModel;
using System.Reflection;
using CloudflareDnsync.Cli.Extensions;
using CloudflareDnsync.Models;
using CloudflareDnsync.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CloudflareDnsync.Cli.Commands;

public sealed class ListCommand(ILogger<ListCommand> logger, IDnsyncConfigService configService) : AsyncCommand<ListCommand.Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var configurations = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Default)
            .StartAsync("Retrieving configurations...", async ctx =>
            {
                if (settings.Id is not null)
                {
                    var configuration = configService.GetById(settings.Id);
                    if (configuration is null)
                    {
                        logger.LogInformation("Configuration not found.");
                        return null;
                    }
                    return [configuration];
                }
                if (settings.Name is not null)
                {
                    var configuration = configService.GetByName(settings.Name);
                    if (configuration is null)
                    {
                        logger.LogInformation("Configuration not found.");
                        return null;
                    }
                    return [configuration];
                }
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
        var configuration = configurations.First();
        if (configurations.Count() > 1)
            configuration = AnsiConsole.Prompt(new SelectionPrompt<DnsyncConfiguration>()
                .Title("Select configuration:".ToTimestampedString())
                .PageSize(10)
                .MoreChoicesText("More")
                .AddChoices(configurations)
                .UseConverter(c => c.Name));
        var table = new Table();
        table.AddColumns("Key", "Value");
        table.Columns[1].Alignment = Justify.Right;
        var properties = configuration.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var propertyInfo in properties)
        {
            if (!settings.RevealToken && propertyInfo.Name == nameof(DnsyncConfiguration.Token))
                continue;
            var value = propertyInfo.GetValue(configuration);
            if (propertyInfo.PropertyType == typeof(bool) && value is not null)
                value = value?.ToString()?.ToLower();
            table.AddRow(propertyInfo.Name, value?.ToString() ?? "null");
        }
        AnsiConsole.Write(table);
        return 0;
    }

    public sealed class Settings : ConfigurationCommand.ConfigurationSettings
    {
        [DefaultValue(false)]
        [Description("Reveal the API token.")]
        [CommandOption("-r|--reveal-token <REVEAL_TOKEN>")]
        public bool RevealToken { get; init; }

        [CommandOption("-i|--id <ID>")]
        [Description("The ID of the configuration.")]
        public string? Id { get; init; }

        [CommandOption("-n|--name <NAME>")]
        [Description("The name of the configuration.")]
        public string? Name { get; init; }
    }
}

