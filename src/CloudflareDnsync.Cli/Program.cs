using CloudflareDnsync.Cli.Commands;
using CloudflareDnsync.Cli.Infrastructure;
using CloudflareDnsync.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Spectre.Console.Cli;

var serviceCollection = new ServiceCollection();

serviceCollection.AddSingleton<IDnsyncConfigService, DnsyncConfigService>();
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

app.Configure(config =>
{
    config.SetApplicationName("cloudflare-dnsync");
    config.SetApplicationVersion("1.0.0");
    config.AddBranch<ConfigurationCommand.ConfigurationSettings>("config", cfg =>
    {
        cfg.AddCommand<AddCommand>("add");
    });
});

return await app.RunAsync(args);
