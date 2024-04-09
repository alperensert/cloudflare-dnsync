# !/bin/sh

version=$(cat ../version.txt)
./bump-version.sh $version

runtimes=($(grep -o '<RuntimeIdentifiers>.*</RuntimeIdentifiers>' ../src/CloudflareDnsync.Cli/CloudflareDnsync.Cli.csproj | sed -e 's/<RuntimeIdentifiers>//g' -e 's/<\/RuntimeIdentifiers>//g' | tr ';' ' '))

for runtime in "${runtimes[@]}"
do
   dotnet publish -c Release -r $runtime -o out/$runtime --self-contained true ../src/CloudflareDnsync.Cli/CloudflareDnsync.Cli.csproj
done

mkdir dist

for runtime in "${runtimes[@]}"
do
   mv out/$runtime/cloudflare-dnsync dist/cloudflare-dnsync-$runtime-$version
done

rm -rf out
