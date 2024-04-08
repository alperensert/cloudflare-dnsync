
using CloudflareDnsync.Cli.Infrastructure;
using CloudflareDnsync.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

var serviceCollection = new ServiceCollection();

serviceCollection.AddSingleton<IDnsyncConfigService, DnsyncConfigService>();

var registrar = new TypeRegistrar(serviceCollection);

var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.SetApplicationName("cloudflare-dnsync");
    config.SetApplicationVersion("1.0.0");
});

return await app.RunAsync(args);
