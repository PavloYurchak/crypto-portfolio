using System.Reflection;
using CryptoPorfolio.Application.Abstractions.Messaging;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoPorfolio.Application
{
    public static class DependencyInjection
    {
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
