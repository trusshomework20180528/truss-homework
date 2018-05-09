# Building and running on Ubuntu 16.04

## Register Microsoft key and feed

```
wget -q packages-microsoft-prod.deb https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
```

## Install .NET SDK and dependencies

```
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-sdk-2.1.103
```

## Verify installation

```
dotnet --version
```

## Build and run in single step

This is easier while iterating during development, but takes longer if you're testing different inputs because it rebuilds every time.

```
dotnet run < sample.csv
```

## Build

```
dotnet build
```

## Run (after build)

```
dotnet bin/Debug/netcoreapp2.0/Truss.dll < sample.csv
```