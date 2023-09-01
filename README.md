# Atomatus Boot Starter Log ⊞📦💻📱📋

[![GitHub issues](https://img.shields.io/github/issues/atomatus/dot-net-boot-starter-log?style=flat-square&color=%232EA043&label=help%20wanted)](https://github.com/atomatus/dot-net-boot-starter-log)

[![NuGet version (Com.Atomatus.BootStarter)](https://img.shields.io/nuget/v/Com.Atomatus.BootStarter.Log.svg?style=flat-square)](https://www.nuget.org/packages/Com.Atomatus.BootStarter.Log/)

**`Com.Atomatus.Bootstarter.Log`** is a C# library that simplifies logging in .NET applications, providing various extensions and configurations for the popular Serilog logger. This library is designed to streamline the setup of logging with Serilog, particularly for scenarios involving Elasticsearch as the log storage.

## Features

- Easy integration with Serilog for logging in .NET applications.
- Simplified setup for logging with Elasticsearch as the log storage.
- Extensions for logging critical, debug, error, information, trace, and warning messages.
- Automatic enrichment of log messages with caller member name, file path, and line number.
- Configuration options for controlling Elasticsearch index naming and other settings.
- Supports both appsettings.json configuration and environment variables.

## Installation

You can install the **Com.Atomatus.Bootstarter.Log** library via NuGet Package Manager or the .NET CLI. Use the following command:

```shell
dotnet add package Com.Atomatus.Bootstarter.Log
```

## Getting Started
Here's a basic example of how to use Com.Atomatus.Bootstarter.Log in a C# application.

#### Step 1: Configure Your Application
First, configure your application to use the library and set up your logging preferences. You can use appsettings.json or environment variables to configure the Elasticsearch URL and other settings.

```csharp
using Com.Atomatus.Bootstarter {

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            builder.UseLogServiceWithElasticsearch(); // Configure logging with Elasticsearch
            var host = builder.Build();

            // Your application code goes here

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Configure your services
                });
    }
}
```

#### Step 2: Use Logging Extensions
Now, you can easily use the logging extensions provided by the library to log messages with different log levels and additional context information. For example:

```csharp
using Com.Atomatus.Bootstarter;
using Microsoft.Extensions.Logging;

public class MyService
{
    private readonly ILogger<MyService> _logger;

    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

    public void DoSomething()
    {
        // Log a critical message
        _logger.LogC("This is a critical message.");

        // Log an error message
        _logger.LogE("An error occurred.");

        // Log an information message
        _logger.LogI("This is an informational message.");

        // Log a warning message
        _logger.LogW("This is a warning message.");

        // Log a debug message
        _logger.LogD("Debugging information.");

        // Log a trace message
        _logger.LogT("Tracing information.");
    }
}

```

## Configuration Options
The library supports various configuration options for controlling Elasticsearch settings:

- **Elasticsearch URL:** You can specify the Elasticsearch URL using either the appsettings.json file or the "ELASTICSEARCH_URL" environment variable. If not provided, the default value is _"http://localhost:9200."_

- **Elasticsearch Index Format:** You can configure the format of the Elasticsearch index name using the "Elasticsearch:Appname" setting in appsettings.json. If not provided, the app name will be automatically determined from the executing assembly name.

- **Number of Replicas:** You can set the number of replicas for Elasticsearch indexes using the "Elasticsearch:NumberOfReplicas" setting in appsettings.json. If not provided, the default value is 1.

## Conclusion
`Com.Atomatus.Bootstarter.Log` is a handy library that simplifies and streamlines logging in .NET applications, especially when using Serilog with Elasticsearch. It offers easy configuration, extensions for different log levels, and enhanced log message context information. By following the steps outlined in this guide, you can quickly get started with efficient logging in your .NET projects.

---

© Atomatus.com. All rights reserved.