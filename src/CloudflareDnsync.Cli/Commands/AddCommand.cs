using System.ComponentModel;
using System.Text.RegularExpressions;
using CloudflareDnsync.Cli.Extensions;
using CloudflareDnsync.Models;
using CloudflareDnsync.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CloudflareDnsync.Cli.Commands;

public sealed partial class AddCommand(ILogger<AddCommand> logger, IDnsyncConfigService configService) : AsyncCommand<AddCommand.Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        string? token = settings.Token;
        if (settings.Token is null)
            token = AnsiConsole.Prompt(new TextPrompt<string>("API token?".ToTimestampedString()).Secret('*'));
        if (token is null)
            throw new InvalidOperationException("API token is required.");
        if (settings.ValidateOptions)
        {
            var isTokenValid = await ValidateToken(token);
            if (!isTokenValid)
            {
                logger.LogError("Token is invalid.");
                return 1;
            }
        }
        if (settings.RecordId is not null && settings.ZoneId is not null && settings.ValidateOptions)
        {
            var isRecordValid = await ValidateRecord(token, settings.ZoneId, settings.RecordId);
            if (!isRecordValid)
            {
                logger.LogError("Record is invalid.");
                return 1;
            }
            var name = PromptName(settings.RecordId, settings);
            while (!IsValidName(name))
            {
                logger.LogError(
                    "Name is invalid. Allowed characters are a-z, A-Z, 0-9, - and . and must not start or end with -.");
                name = PromptName(settings.RecordId, settings);
            }
            try
            {
                await configService.AddAsync(new DnsyncConfiguration
                {
                    Name = name,
                    Token = token,
                    ZoneId = settings.ZoneId,
                    Id = settings.RecordId,
                    UseProxy = settings.Proxy,
                    Type = DnsyncConfiguration.RecordType.A
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add configuration.");
                return 1;

            }
        }
        else
        {
            using var cf = new CloudflareService(token);
            var prompts = await PromptZoneAndRecord(cf);
            if (prompts is null)
                return 1;
            var (zone, record, proxy) = prompts.Value;
            var name = PromptName(record.Name, settings);
            while (!IsValidName(name))
            {
                logger.LogError(
                    "Name is invalid. Allowed characters are a-z, A-Z, 0-9, - and . and must not start or end with -.");
                name = PromptName(record.Name, settings);
            }
            try
            {
                await configService.AddAsync(new DnsyncConfiguration
                {
                    Name = name,
                    Token = token,
                    ZoneId = zone.Id,
                    Id = record.Id,
                    UseProxy = proxy,
                    Type = record.Type switch
                    {
                        "A" => DnsyncConfiguration.RecordType.A,
                        "AAAA" => DnsyncConfiguration.RecordType.AAAA,
                        _ => throw new InvalidOperationException("Unknown record type.")
                    }
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add configuration.");
                return 1;
            }
        }
        logger.LogInformation("Configuration added successfully.");
        return 0;
    }

    private static bool IsValidName(string name) => Settings.NameRegex.IsMatch(name);

    private static string PromptName(string recordName, Settings settings)
    {
        if (settings.Name is not null)
            return settings.Name;
        return AnsiConsole.Prompt(new TextPrompt<string>("Configuration Name?".ToTimestampedString())
            .DefaultValue(recordName));
    }

    private async Task<(Zone zone, DnsRecord record, bool proxy)?> PromptZoneAndRecord(ICloudflareService cloudflareService)
    {
        var zone = await AnsiConsole.Status()
            .StartAsync("Fetching zones", async ctx =>
            {
                var zones = await cloudflareService.GetZonesAsync();
                if (!zones.Success)
                {
                    logger.LogError("Failed to fetch zones.");
                    return null;
                }
                await Task.Delay(250);
                ctx.Status("Waiting for user input");
                return AnsiConsole.Prompt(new SelectionPrompt<Zone>()
                    .Title("Zone ID?".ToTimestampedString())
                    .PageSize(20)
                    .AddChoices(zones.Result)
                    .UseConverter(z => z.Name));
            });
        if (zone is null)
            return null;
        logger.LogInformation("Selected zone: {name} ({id})", zone.Name, zone.Id);
        var record = await AnsiConsole.Status()
            .StartAsync($"Fetching records from {zone.Name}", async ctx =>
        {
            var records = await cloudflareService.GetDnsRecordsAsync(zone.Id);
            await Task.Delay(250);
            if (!records.Success)
            {
                logger.LogError("Failed to fetch records.");
                return null;
            }
            ctx.Status("Waiting for user input");
            return AnsiConsole.Prompt(new SelectionPrompt<DnsRecord>()
                .Title("Record ID?".ToTimestampedString())
                .PageSize(20)
                .AddChoices(records.Result)
                .UseConverter(r => r.Name));
        });
        if (record is null)
            return null;
        logger.LogInformation("Selected record: {name} ({id})", record.Name, record.Id);
        if (record.Proxiable)
        {
            var proxy = AnsiConsole.Prompt(new ConfirmationPrompt(
                "Do you want to use proxy for this record?".ToTimestampedString()
            ));
            logger.LogInformation("Proxy: {proxy}", proxy);
            return (zone, record, proxy);
        }
        return (zone, record, false);
    }

    private Task<bool> ValidateRecord(string token, string zoneId, string recordId)
    {
        return AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots11)
                .StartAsync("Validating record", async ctx =>
                {
                    logger.LogInformation("Validating record");
                    ctx.Status("Creating cloudflare service");
                    using var cf = new CloudflareService(token);
                    await Task.Delay(250);
                    ctx.Status("Verifying record via cloudflare");
                    var response = await cf.GetDnsRecordAsync(zoneId, recordId);
                    await Task.Delay(250);
                    return response.Success;
                });
    }

    private Task<bool> ValidateToken(string token)
    {
        return AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots11)
                .StartAsync("Validating token", async ctx =>
                {
                    logger.LogInformation("Validating token");
                    ctx.Status("Creating cloudflare service");
                    using var cf = new CloudflareService(token);
                    await Task.Delay(250);
                    ctx.Status("Verifying token via cloudflare");
                    var response = await cf.VerifyTokenAsync();
                    await Task.Delay(250);
                    return response.Success;
                });
    }

    public sealed partial class Settings : ConfigurationCommand.ConfigurationSettings
    {
        public static Regex NameRegex = ConfigurationNameRegex();

        [Description("Configuration name.")]
        [CommandOption("-n|--name <NAME>")]
        public string? Name { get; init; }

        [Description("API token to use.")]
        [CommandOption("-t|--token <TOKEN>")]
        public string? Token { get; init; }

        [Description("Zone ID to add. If specified, record id must also be specified.")]
        [CommandOption("-z|--zone <ZONE>")]
        public string? ZoneId { get; init; }

        [Description("Record ID to add. If specified, zone id must also be specified.")]
        [CommandOption("-r|--record <RECORD>")]
        public string? RecordId { get; init; }

        [Description("Use proxy for the record.")]
        [CommandOption("-p|--proxy <PROXY>")]
        public bool Proxy { get; init; } = false;

        [DefaultValue(true)]
        [Description("Validate options. If set to false, no validation will be performed.")]
        [CommandOption("--validate <VALIDATE>")]
        public bool ValidateOptions { get; init; }

        public override ValidationResult Validate()
        {
            if ((ZoneId is null && RecordId is not null) || (ZoneId is not null && RecordId is null))
                return ValidationResult.Error("Both zone id and record id must be specified.");
            if (Name is not null && !NameRegex.IsMatch(Name))
                return ValidationResult.Error("Name is invalid. Allowed characters are a-z, A-Z, 0-9, - and . and must not start or end with -.");
            return ValidationResult.Success();
        }

        [GeneratedRegex(@"^(?!.*-$)(?!.*-$)[a-zA-Z0-9](?:[a-zA-Z0-9-.]{1,})[a-zA-Z0-9]$")]
        private static partial Regex ConfigurationNameRegex();
    }
}
