#!/bin/bash

VERSION=$1

if [[ "$OSTYPE" == "darwin"* ]]; then
    sed -i "" "s/<Version>.*<\/Version>/<Version>$VERSION<\/Version>/" ../src/CloudflareDnsync.Cli/CloudflareDnsync.Cli.csproj
else
    sed -i "s/<Version>.*<\/Version>/<Version>$VERSION<\/Version>/" ../src/CloudflareDnsync.Cli/CloudflareDnsync.Cli.csproj
fi
