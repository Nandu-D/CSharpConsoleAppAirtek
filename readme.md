# Console App in C#

## To run the console application execute the following command in "projectRoot/AirTek" folder

```
dotnet run
```

## To build execute the following command in "projectRoot/" folder

```
dotnet build
```

## To run unit tests execute the following command in "projectRoot/" folder

```
dotnet test
```

## To create a single executable for windows, run the following command

```
dotnet publish -r  win-x64  -c Release -p:PublishSingleFile=true --self-contained true
```

Files are going to be generated in the folder "/AirTek/AirTek/bin/Release/net7.0/win-x64/publish/"

## To create a single executable for linux, run the following command

```
dotnet publish -r linux-x64  -c Release -p:PublishSingleFile=true --self-contained true
```

Files are going to be generated in the folder "/AirTek/AirTek/bin/Release/net7.0/linux-x64/publish/"

## To create a single executable for MacOS Intel, run the following command

```
dotnet publish -r osx-x64 -c Release -p:PublishSingleFile=true --self-contained true
```

Files are going to be generated in the folder "/AirTek/AirTek/bin/Release/net7.0/osx-x64/publish/"

## To create a single executable for MacOS Apple Silicone, run the following command

```
dotnet publish -r osx-arm64  -c Release -p:PublishSingleFile=true --self-contained true
```

Files are going to be generated in the folder "/AirTek/AirTek/bin/Release/net7.0/osx-arm64/publish/"

## Screenshot of output

![Alt text](/ScreenshotOfOutput.png?raw=true "Screenshot of output")
