#!/bin/bash

VERSION=$1
sed -i "" "s/<Version>.*<\/Version>/<Version>$VERSION<\/Version>/" ./CloudflareDnsync.Cli.csproj
