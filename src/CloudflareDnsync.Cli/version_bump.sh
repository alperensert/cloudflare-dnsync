#!/bin/bash

VERSION=$1
if [[ "$OSTYPE" == "darwin"* ]]; then
    sed -i "" "s/<Version>.*<\/Version>/<Version>$VERSION<\/Version>/" CloudflareDnsync.Cli.csproj
else
    sed -i "s/<Version>.*<\/Version>/<Version>$VERSION<\/Version>/" CloudflareDnsync.Cli.csproj
fi
