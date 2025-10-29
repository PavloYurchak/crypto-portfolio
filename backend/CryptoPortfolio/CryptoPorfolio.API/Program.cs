// <copyright file="Program.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application;
using CryptoPorfolio.Infrastructure;
using CryptoPorfolio.Infrastructure.Abstraction;
using Microsoft.OpenApi.Models;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication(configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CryptoPortfolio API",
        Version = "v1",
    });
});

var app = builder.Build();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

await InitializeDatabase(app);

app.Run();
static async Task InitializeDatabase(WebApplication app, CancellationToken cancellationToken = default(CancellationToken))
{
    using IServiceScope scope = app.Services.CreateScope();
    IDatabaseInitializer service = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    if (service != null)
    {
        await service.InitializeAndSeed(cancellationToken);
    }

    scope.Dispose();
}
