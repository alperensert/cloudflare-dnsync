#!/bin/bash

VERSION=$1
sed -i "" "s/<Version>.*<\/Version>/<Version>$VERSION<\/Version>/" src/CloudflareDnsync.Cli/CloudflareDnsync.Cli.csproj
