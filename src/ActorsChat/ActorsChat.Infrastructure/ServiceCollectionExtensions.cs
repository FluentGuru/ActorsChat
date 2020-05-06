using ActorsChat.Domain.Services;
using ActorsChat.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ActorsChat.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IHasher, Sha1Hasher>();
            services.AddSingleton<IDateTime, MachineDateTime>();
            return services;
        }
    }
}
