// <copyright file="DependencyInjection.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Security;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Domain.Services;
using CryptoPorfolio.Infrastructure.Abstraction;
using CryptoPorfolio.Infrastructure.Context;
using CryptoPorfolio.Infrastructure.Repositories;
using CryptoPorfolio.Infrastructure.Services;
using CryptoPorfolio.Infrastructure.Services.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoPorfolio.Infrastructure
{
    /// <summary>
    /// Dependency injection extension methods for the Infrastructure layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds Infrastructure services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The service collection to which the infrastructure services will be added.</param>
        /// <param name="configuration">The application configuration used to configure the services.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var dbProvider = configuration["Database:Provider"] ?? "SqlServer";
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CryptoPorfolioContext>(options =>
            {
                switch (dbProvider.ToLower())
                {
                    case "SqlServer":
                    default:
                        options.UseSqlServer(connectionString);
                        break;
                }
            });

            services.AddScoped<IDatabaseInitializer, DatabaseInitializer<CryptoPorfolioContext>>();

            services.Scan(scan => scan
                .FromAssemblies(typeof(UserRepository).Assembly)
                .AddClasses(c => c.AssignableTo<IDomainRepository>(), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IDbTransactionService, DbTransactionService>();

            return services;
        }
    }
}
