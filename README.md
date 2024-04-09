# cloudflare-dnsync

Cloudflare Dnsync is a basic CLI tool for updating Cloudflare dns records to the latest ip of your device.

### Installation
```sh
dotnet tool install cloudflare-dnsync --global
```

### Available Commands
- Synchronize all out-dated records: `cloudflare-dnsync sync`
- Add new dns record to the configuration: `cloudflare-dnsync config add`
- Remove a dns record from the configuration: `cloudflare-dnsync config rm`
- List dns record configurations: `cloudflare-dnsync config ls`

### Motivation
I purchased a [Bee-Link Mini S12](https://amzn.eu/d/a3wNyZN) to serve as my home server. Despite being unable to obtain a static IP address from my internet provider, I developed this project as a CLI tool to automatically update my DNS records.

### Licensing
cloudflare-dnsync is under [MIT license](/LICENSE).
