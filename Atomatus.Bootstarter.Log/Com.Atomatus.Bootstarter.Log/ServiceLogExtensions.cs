using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Com.Atomatus.Bootstarter
{
    public static class ServiceLogExtensions
    {
        private static readonly EnvironmentVariable ElasticsearchUrl = "ELASTICSEARCH_URL";

        public static IServiceCollection AddLogService([NotNull] this IServiceCollection services)
        {
            return services
                .AddSingleton(prov => Log.Logger);
        }

        /// <summary>
        /// To use log with elasticsearch inputing configuration 
        /// values, create into <i>appsettings.json</i> project 
        /// the follow field:
        /// <code>
        /// "Elasticsearch": {<br/>
        ///  "Url": "http://localhost:5151/elasticsearch",<br/>
        ///  "Appname":  "YourAppNameInElasticsearch"<br/>
        /// },
        /// </code>
        /// or create the follow simple field within only elasticsearch url,
        /// then the app name will be the current execunting assembly name.
        /// <code>
        /// "Elasticsearch": "http://localhost:5151/elasticsearch"
        /// </code>
        /// <para>
        /// or create an environment variable to elasticsearch url.<br/><br/>
        /// "ELASTICSEARCH_URL": "http://localhost:5151/elasticsearch"
        /// </para>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IHostBuilder UseLogServiceWithElasticsearch([NotNull] this IHostBuilder builder)
        {
            return builder
                .ConfigureServices((hc, services) => services.AddLogServiceWithElasticsearch(hc.Configuration))
                .ConfigureLogging((hc, builder) =>
                    builder.AddSerilog(
                        builder.Services
                            .BuildServiceProvider()
                            .GetRequiredService<Serilog.ILogger>(), dispose: true));
        }

        /// <summary>
        /// To use log with elasticsearch inputing configuration 
        /// values, create into <i>appsettings.json</i> project 
        /// the follow field:
        /// <code>
        /// "Elasticsearch": {<br/>
        ///  "Url": "http://localhost:5151/elasticsearch",<br/>
        ///  "Appname":  "YourAppNameInElasticsearch"<br/>
        /// },
        /// </code>
        /// or create the follow simple field within only elasticsearch url,
        /// then the app name will be the current execunting assembly name.
        /// <code>
        /// "Elasticsearch": "http://localhost:5151/elasticsearch"
        /// </code>
        /// <para>
        /// or create an environment variable to elasticsearch url.<br/><br/>
        /// "ELASTICSEARCH_URL": "http://localhost:5151/elasticsearch"
        /// </para>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLogServiceWithElasticsearch([NotNull] this IServiceCollection services, [AllowNull] IConfiguration configuration = null)
        {
            if(configuration is null)
            {
                var provider  = services.BuildServiceProvider();
                configuration = provider.GetService<IConfiguration>();
            }

            string elasticsearchUrl = ElasticsearchUrl.GetValueOrDefault(configuration?["Elasticsearch:Url"] ?? configuration?["Elasticsearch"]);
            string appName = configuration?["Elasticsearch:Appname"] ?? Assembly.GetEntryAssembly().GetName().Name;

            if (string.IsNullOrWhiteSpace(elasticsearchUrl))
            {
                throw new InvalidOperationException("Is not possible initialize log with elasticsearch because:\n" +
                    "Elasticsearch url is not set in appsettings.json nor like environment variable.\n\n" +
                    "\t-> To set on appsetings.json use the following pattern:\n" +
                    "\t\t'\"Elastiseach\": \"https://elasticsearch_url\"' or '\"Elastiseach\": { \"Url\": \"https://elasticsearch_url\", \"Appname\": \"My app name\" }'\n\n" +
                    "\t-> To set as environment variable use:\n" +
                    "\t\t \"ELASTICSEARCH_URL\"=\"https://elasticsearch_url\"");
            }

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticsearchUrl))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                    IndexFormat = $"{appName}-{DateTime.Now:yyyy-MM-dd}",
                    NumberOfReplicas = 1
                }).CreateLogger();

            return services.AddLogService();
        }

        /// <summary>
        /// To use log with elasticsearch inputing configuration 
        /// values, create into <i>appsettings.json</i> project 
        /// the follow field:
        /// <code>
        /// "Elasticsearch": {<br/>
        ///  "Url": "http://localhost:5151/elasticsearch",<br/>
        ///  "Appname":  "YourAppNameInElasticsearch"<br/>
        /// },
        /// </code>
        /// or create the follow simple field within only elasticsearch url,
        /// then the app name will be the current execunting assembly name.
        /// <code>
        /// "Elasticsearch": "http://localhost:5151/elasticsearch"
        /// </code>
        /// <para>
        /// or create an environment variable to elasticsearch url.<br/><br/>
        /// "ELASTICSEARCH_URL": "http://localhost:5151/elasticsearch"
        /// </para>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ILoggerFactory UseLogServiceWithElasticsearch([NotNull] this ILoggerFactory loggerFactory)
        {
            return loggerFactory.AddSerilog(dispose: true);
        }

        /// <summary>
        /// To use log with elasticsearch inputing configuration 
        /// values, create into <i>appsettings.json</i> project 
        /// the follow field:
        /// <code>
        /// "Elasticsearch": {<br/>
        ///  "Url": "http://localhost:5151/elasticsearch",<br/>
        ///  "Appname":  "YourAppNameInElasticsearch"<br/>
        /// },
        /// </code>
        /// or create the follow simple field within only elasticsearch url,
        /// then the app name will be the current execunting assembly name.
        /// <code>
        /// "Elasticsearch": "http://localhost:5151/elasticsearch"
        /// </code>
        /// <para>
        /// or create an environment variable to elasticsearch url.<br/><br/>
        /// "ELASTICSEARCH_URL": "http://localhost:5151/elasticsearch"
        /// </para>
        /// </summary>
        /// <param name="app"></param>
        /// <param name="loggerFactory"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLogServiceWithElasticsearch([NotNull] IApplicationBuilder app, [AllowNull] ILoggerFactory loggerFactory = null)
        {
            loggerFactory ??= app.ApplicationServices.GetService<ILoggerFactory>();
            loggerFactory.UseLogServiceWithElasticsearch();
            return app;
        }
    }
}
