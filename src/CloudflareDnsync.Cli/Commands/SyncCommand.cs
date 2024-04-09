using CloudflareDnsync.Models;
using CloudflareDnsync.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CloudflareDnsync.Cli.Commands;

public sealed class SyncCommand(
    ILogger<SyncCommand> logger,
    IDnsyncConfigService configService,
    IIPService ipService)
    : AsyncCommand<SyncCommand.Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        logger.LogInformation("Retrieving configurations for synchronization");
        var configurations = configService.GetAll().Where(c => c.Enabled).ToList();
        if (configurations.Count == 0)
        {
            logger.LogWarning("No configurations found for synchronization");
            return 0;
        }
        logger.LogInformation("Found {Count} configurations", configurations.Count);
        var ip = await AnsiConsole.Status()
            .StartAsync("Retrieving public IP address..", async ctx =>
            {
                var ip = await ipService.GetPublicIpAsync();
                logger.LogInformation("Public IP address: {Ip}", ip);
                ctx.Status("Public IP address retrieved");
                return ip;
            });
        var outDatedConfigs = configurations.Where(c => c.Content != ip).ToList();
        if (outDatedConfigs.Count == 0)
        {
            logger.LogInformation("All configurations are up-to-date");
            return 0;
        }
        logger.LogInformation("Found {Count} configurations that are out-of-date", outDatedConfigs.Count);
        foreach (var config in configurations)
        {
            using var cf = CreateCfInstance(config);
            try
            {
                await AnsiConsole.Status()
                    .StartAsync($"Synchorizing configuration: {config.Name}", async ctx =>
                    {
                        config.RecordName ??= await GetRecordName(cf, config.ZoneId, config.Id);
                        var request = new DnsRecordUpdateRequest("A", config.RecordName, ip)
                        {
                            Proxied = config.UseProxy,
                        };
                        ctx.Status("Sending update request to Cloudflare..");
                        var response = await cf.UpdateDnsRecordAsync(config.ZoneId, config.Id, request);
                        if (!response.Success)
                            throw new InvalidOperationException("Failed to update DNS record");
                        config.Content = ip;
                        await Task.Delay(250);
                        ctx.Status("Updating configuration..");
                        await configService.UpdateAsync(config);
                        ctx.Status("Configuration updated");
                    });
                logger.LogInformation("Configuration synchronized: {name}", config.Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to synchronize configuration: {name}", config.Name);
            }
        }
        return 0;
    }

    private static async Task<string> GetRecordName(CloudflareService cf, string zoneId, string recordId)
    {
        var record = await cf.GetDnsRecordAsync(zoneId, recordId);
        if (!record.Success)
            throw new InvalidOperationException("Record not found");
        return record.Result.Name;
    }

    private static CloudflareService CreateCfInstance(DnsyncConfiguration config)
    {
        if (config.IsUsingToken)
            return new CloudflareService(config.Token);
        return new CloudflareService(config.ApiEmail, config.ApiKey);
    }

    public sealed class Settings : CommandSettings
    {
    }
}
