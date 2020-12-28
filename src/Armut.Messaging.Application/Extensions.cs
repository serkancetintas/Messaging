using Armut.Messaging.Application.Commands;
using Armut.Messaging.Application.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Armut.Messaging.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCommandHandlers();
            services.AddQueryHandlers();

            return services;
        }

        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.Scan(s =>
               s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                   .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime());

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
    }
}
