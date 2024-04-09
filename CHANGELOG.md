# Changelog

## [1.0.0](https://github.com/alperensert/cloudflare-dnsync/compare/v1.0.1...1.0.0) (2024-04-09)


### Features

* add CloudflareDnsync.Models project to the solution ([9adc33a](https://github.com/alperensert/cloudflare-dnsync/commit/9adc33a30fa4afdff280e318f0c666a9ff964b96))
* add CloudflareResponse and Zone classes ([4977ac5](https://github.com/alperensert/cloudflare-dnsync/commit/4977ac54d4d024e194a8b955930e0230b3435e1c))
* add configuration and it's subcommand add command ([efab56f](https://github.com/alperensert/cloudflare-dnsync/commit/efab56f9a5d5f6157662c50dd9ae78f445a459b2))
* add configuration related methods ([c112792](https://github.com/alperensert/cloudflare-dnsync/commit/c112792544466af308f34edd51036362f4014324))
* add configuration service and its model class ([593d298](https://github.com/alperensert/cloudflare-dnsync/commit/593d2983917a7f830c63402cdca05de27c9ae2d1))
* add GetDnsRecordAsync method to CloudflareService ([37bff3c](https://github.com/alperensert/cloudflare-dnsync/commit/37bff3cddb2da0c049f187a847928735f61450dd))
* add GetDnsRecordsAsync method ([27f5be0](https://github.com/alperensert/cloudflare-dnsync/commit/27f5be0e899c6fdc36d0b35335fd71d421b7d75c))
* add GetZonesAsync method to CloudflareService ([4f6fdbd](https://github.com/alperensert/cloudflare-dnsync/commit/4f6fdbde8e8d8f27718009314961165cae3952e7))
* add StringExtensions static class for timestamped string formatting ([6d355bc](https://github.com/alperensert/cloudflare-dnsync/commit/6d355bc2fd89e5a28f81799ad048177a2ad055fb))
* add TokenVerify class and VerifyTokenAsync method ([8343103](https://github.com/alperensert/cloudflare-dnsync/commit/83431036b9b419666d26d5c5d7ecd7bac01173e8))
* add UpdateDnsRecordAsync method and DnsRecordUpdateRequest class ([9880190](https://github.com/alperensert/cloudflare-dnsync/commit/9880190126b5080e23daf4f86d0360b2403de38d))
* added Serilog for better logging experience ([5c2e370](https://github.com/alperensert/cloudflare-dnsync/commit/5c2e37003b2887f5cb77caae714169acd336e7f5))
* implement add command to saving configurations to file ([6176232](https://github.com/alperensert/cloudflare-dnsync/commit/6176232879fe61218df1bda603e878029aa6b084))
* implement full functionality of sync command ([c87953b](https://github.com/alperensert/cloudflare-dnsync/commit/c87953bdf081aed85d04bb4b07cb426790d68224))
* ip service for retrieving external ip from providers ([ee647df](https://github.com/alperensert/cloudflare-dnsync/commit/ee647dfd19fa02aeb898a652c14f4fec512a16dc))
* list command for retrieving configurations ([8322a7e](https://github.com/alperensert/cloudflare-dnsync/commit/8322a7e11cc6929d12d9049b9cd6c5685164868d))
* refactor cli project to add required package references and infrastructure classes ([126d65d](https://github.com/alperensert/cloudflare-dnsync/commit/126d65d359d3863a12715790d9870ccb31cec2ad))
* remove command for removing configurations ([83e47cc](https://github.com/alperensert/cloudflare-dnsync/commit/83e47cc9dfef3a28bd7a5f57659502cfdf6436fd))


### Bug Fixes

* http method is fixed for send async method ([d47731f](https://github.com/alperensert/cloudflare-dnsync/commit/d47731fe7c14e908c094fd0a59e3d3f6b762743a))
* nullability annotation to support code analysis ([dbecb6b](https://github.com/alperensert/cloudflare-dnsync/commit/dbecb6b2db8d0b9dd3bf7459eff973ab3725a4a0))
* version bump shell script fixed ([5f97eb7](https://github.com/alperensert/cloudflare-dnsync/commit/5f97eb7fff467879d9a03a8973f6b6a2fe2cc99f))


### Miscellaneous Chores

* release 1.0.0 ([77d69c2](https://github.com/alperensert/cloudflare-dnsync/commit/77d69c24f47f8d274b3c332af4ecd9835b66330c))

## [1.0.1](https://github.com/alperensert/cloudflare-dnsync/compare/v1.0.0...1.0.1) (2024-04-09)


### Bug Fixes

* version bump shell script fixed ([5f97eb7](https://github.com/alperensert/cloudflare-dnsync/commit/5f97eb7fff467879d9a03a8973f6b6a2fe2cc99f))

## 1.0.0 (2024-04-09)


### Features

* add CloudflareDnsync.Models project to the solution ([9adc33a](https://github.com/alperensert/cloudflare-dnsync/commit/9adc33a30fa4afdff280e318f0c666a9ff964b96))
* add CloudflareResponse and Zone classes ([4977ac5](https://github.com/alperensert/cloudflare-dnsync/commit/4977ac54d4d024e194a8b955930e0230b3435e1c))
* add configuration and it's subcommand add command ([efab56f](https://github.com/alperensert/cloudflare-dnsync/commit/efab56f9a5d5f6157662c50dd9ae78f445a459b2))
* add configuration related methods ([c112792](https://github.com/alperensert/cloudflare-dnsync/commit/c112792544466af308f34edd51036362f4014324))
* add configuration service and its model class ([593d298](https://github.com/alperensert/cloudflare-dnsync/commit/593d2983917a7f830c63402cdca05de27c9ae2d1))
* add GetDnsRecordAsync method to CloudflareService ([37bff3c](https://github.com/alperensert/cloudflare-dnsync/commit/37bff3cddb2da0c049f187a847928735f61450dd))
* add GetDnsRecordsAsync method ([27f5be0](https://github.com/alperensert/cloudflare-dnsync/commit/27f5be0e899c6fdc36d0b35335fd71d421b7d75c))
* add GetZonesAsync method to CloudflareService ([4f6fdbd](https://github.com/alperensert/cloudflare-dnsync/commit/4f6fdbde8e8d8f27718009314961165cae3952e7))
* add StringExtensions static class for timestamped string formatting ([6d355bc](https://github.com/alperensert/cloudflare-dnsync/commit/6d355bc2fd89e5a28f81799ad048177a2ad055fb))
* add TokenVerify class and VerifyTokenAsync method ([8343103](https://github.com/alperensert/cloudflare-dnsync/commit/83431036b9b419666d26d5c5d7ecd7bac01173e8))
* add UpdateDnsRecordAsync method and DnsRecordUpdateRequest class ([9880190](https://github.com/alperensert/cloudflare-dnsync/commit/9880190126b5080e23daf4f86d0360b2403de38d))
* added Serilog for better logging experience ([5c2e370](https://github.com/alperensert/cloudflare-dnsync/commit/5c2e37003b2887f5cb77caae714169acd336e7f5))
* implement add command to saving configurations to file ([6176232](https://github.com/alperensert/cloudflare-dnsync/commit/6176232879fe61218df1bda603e878029aa6b084))
* implement full functionality of sync command ([c87953b](https://github.com/alperensert/cloudflare-dnsync/commit/c87953bdf081aed85d04bb4b07cb426790d68224))
* ip service for retrieving external ip from providers ([ee647df](https://github.com/alperensert/cloudflare-dnsync/commit/ee647dfd19fa02aeb898a652c14f4fec512a16dc))
* list command for retrieving configurations ([8322a7e](https://github.com/alperensert/cloudflare-dnsync/commit/8322a7e11cc6929d12d9049b9cd6c5685164868d))
* refactor cli project to add required package references and infrastructure classes ([126d65d](https://github.com/alperensert/cloudflare-dnsync/commit/126d65d359d3863a12715790d9870ccb31cec2ad))
* remove command for removing configurations ([83e47cc](https://github.com/alperensert/cloudflare-dnsync/commit/83e47cc9dfef3a28bd7a5f57659502cfdf6436fd))


### Bug Fixes

* http method is fixed for send async method ([d47731f](https://github.com/alperensert/cloudflare-dnsync/commit/d47731fe7c14e908c094fd0a59e3d3f6b762743a))
* nullability annotation to support code analysis ([dbecb6b](https://github.com/alperensert/cloudflare-dnsync/commit/dbecb6b2db8d0b9dd3bf7459eff973ab3725a4a0))
