using Armut.Messaging.Application.Queries;
using Armut.Messaging.Infrastructure.Exceptions;
using Armut.Messaging.Infrastructure.Logging;
using Armut.Messaging.Infrastructure.Logging.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Armut.Messaging.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddQueryHandlers();
            services.AddErrorHandler<ExceptionToResponseMapper>();

            return services;
        }

        public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            services.Scan(s =>
               s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                   .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime());

            return services;
        }

        public static IServiceCollection AddErrorHandler<T>(this IServiceCollection services)
             where T : class, IExceptionToResponseMapper
        {
            services.AddTransient<ErrorHandlerMiddleware>();
            services.AddSingleton<IExceptionToResponseMapper, T>();

            return services;
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
          => builder.UseMiddleware<ErrorHandlerMiddleware>();

        public static string Underscore(this string value)
           => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()))
               .ToLowerInvariant();

        internal static LoggingLevelSwitch LoggingLevelSwitch = new LoggingLevelSwitch();
        internal static LogEventLevel GetLogEventLevel(string level)
          => Enum.TryParse<LogEventLevel>(level, true, out var logLevel)
              ? logLevel
              : LogEventLevel.Information;

        public static IHostBuilder UseLogging(this IHostBuilder webHostBuilder,
           Action<LoggerConfiguration> configure = null, string loggerSectionName = "logger",
           string appSectionName = "app")
           => webHostBuilder
               .ConfigureServices(services => services.AddSingleton<ILoggingService, LoggingService>())
               .UseSerilog((context, loggerConfiguration) =>
               {
                   var loggerOptions = context.Configuration.GetOptions<LoggerOptions>(loggerSectionName);

                   MapOptions(loggerOptions, loggerConfiguration,
                       context.HostingEnvironment.EnvironmentName);
                   configure?.Invoke(loggerConfiguration);
               });

        private static void MapOptions(LoggerOptions loggerOptions, LoggerConfiguration loggerConfiguration, string environmentName)
        {
            LoggingLevelSwitch.MinimumLevel = GetLogEventLevel(loggerOptions.Level);

            loggerConfiguration.Enrich.FromLogContext()
                .MinimumLevel.ControlledBy(LoggingLevelSwitch)
                .Enrich.WithProperty("Environment", environmentName)
                .Enrich.WithProperty("Application", "Messaging")
                .Enrich.WithProperty("Version", "1.0");

            foreach (var (key, value) in loggerOptions.Tags ?? new Dictionary<string, object>())
            {
                loggerConfiguration.Enrich.WithProperty(key, value);
            }

            foreach (var (key, value) in loggerOptions.MinimumLevelOverrides ?? new Dictionary<string, string>())
            {
                var logLevel = GetLogEventLevel(value);
                loggerConfiguration.MinimumLevel.Override(key, logLevel);
            }

            loggerOptions.ExcludePaths?.ToList().ForEach(p => loggerConfiguration.Filter
                .ByExcluding(Matching.WithProperty<string>("RequestPath", n => n.EndsWith(p))));

            loggerOptions.ExcludeProperties?.ToList().ForEach(p => loggerConfiguration.Filter
                .ByExcluding(Matching.WithProperty(p)));

            Configure(loggerConfiguration, loggerOptions);
        }

        private static void Configure(LoggerConfiguration loggerConfiguration,
          LoggerOptions options)
        {
            var consoleOptions = options.Console ?? new ConsoleOptions();
            var fileOptions = options.File ?? new FileOptions();
            var seqOptions = options.Seq ?? new SeqOptions();

            if (consoleOptions.Enabled)
            {
                loggerConfiguration.WriteTo.Console();
            }

            if (fileOptions.Enabled)
            {
                var path = string.IsNullOrWhiteSpace(fileOptions.Path) ? "logs/logs.txt" : fileOptions.Path;
                if (!Enum.TryParse<RollingInterval>(fileOptions.Interval, true, out var interval))
                {
                    interval = RollingInterval.Day;
                }

                loggerConfiguration.WriteTo.File(path, rollingInterval: interval);
            }

            if (seqOptions.Enabled)
            {
                loggerConfiguration.WriteTo.Seq(seqOptions.Url, apiKey: seqOptions.ApiKey);
            }
        }

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
            where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }
    }
}
