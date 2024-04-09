using CloudflareDnsync.Cli.Commands;
using CloudflareDnsync.Cli.Infrastructure;
using CloudflareDnsync.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Spectre.Console.Cli;

var serviceCollection = new ServiceCollection();

serviceCollection.AddSingleton<IDnsyncConfigService, DnsyncConfigService>();
serviceCollection.AddSingleton<IIPService, IPService>();
serviceCollection.AddLogging(builder =>
{
    var logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateLogger();
    builder.AddSerilog(logger);
});

var registrar = new TypeRegistrar(serviceCollection);

var app = new CommandApp(registrar);

app.SetDefaultCommand<SyncCommand>().WithDescription("Sync DNS records with Cloudflare");
app.Configure(config =>
{
    config.SetApplicationName("cloudflare-dnsync");
    config.AddCommand<SyncCommand>("sync")
        .WithDescription("Sync DNS records with Cloudflare");
    config.AddBranch<ConfigurationCommand.ConfigurationSettings>("config", cfg =>
    {
        cfg.SetDefaultCommand<ListCommand>();
        cfg.AddCommand<AddCommand>("add");
        cfg.AddCommand<ListCommand>("list")
            .WithAlias("ls");
        cfg.AddCommand<RemoveCommand>("remove")
            .WithAlias("rm");
    }).WithAlias("cfg");
});

return await app.RunAsync(args);
