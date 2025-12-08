// <copyright file="DependencyInjection.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using System.Reflection;
using CryptoPorfolio.Application.Abstractions.Messaging;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoPorfolio.Application
{
    /// <summary>
    /// Dependency injection extension methods for the Application layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds Application services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to which the Application services will be added.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var asm = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(asm);

            services.Scan(scan => scan
            .FromAssemblies(asm)
            .AddClasses(c => c.AssignableTo(typeof(IHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped<ISender, MessageBus>();

            return services;
        }
    }
}
