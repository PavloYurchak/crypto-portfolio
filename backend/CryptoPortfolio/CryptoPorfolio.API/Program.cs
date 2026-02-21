// <copyright file="Program.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using System.Reflection;
using System.Text;
using CryptoPorfolio.Application;
using CryptoPorfolio.Application.Abstractions.Security;
using CryptoPorfolio.Infrastructure;
using CryptoPorfolio.Infrastructure.Abstraction;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication(configuration);
builder.Services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSection = builder.Configuration.GetSection("Jwt");
        var jwtOptions = jwtSection.Get<JwtOptions>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtOptions?.Issuer,
            ValidAudience = jwtOptions?.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions?.Key!)),
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CryptoPortfolio API",
        Version = "v1",
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "¬веди т≥льки JWT токен без преф≥кса 'Bearer '",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            Array.Empty<string>()
        },
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

app.UseAuthentication();
app.UseAuthorization();

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
