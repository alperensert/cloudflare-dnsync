using CloudflareDnsync.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

namespace CloudflareDnsync.Cli.Commands;

public sealed class SyncCommand(ILogger<SyncCommand> logger, IDnsyncConfigService configService)
    : AsyncCommand<SyncCommand.Settings>
{
    public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        throw new NotImplementedException();
    }

    public sealed class Settings : CommandSettings
    {
    }
}
