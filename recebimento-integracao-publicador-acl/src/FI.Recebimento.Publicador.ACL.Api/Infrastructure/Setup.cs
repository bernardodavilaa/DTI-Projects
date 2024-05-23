using FI.Recebimento.Publicador.ACL.Api.Infrastructure.RabbitMq;
using Localiza.BuildingBlocks.HealthChecks.Configuration;
using FI.Recebimento.Publicador.ACL.Infrastructure;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using FI.Recebimento.Publicador.ACL.Domain.AggregatesModel;

namespace FI.Recebimento.Publicador.ACL.Infrastructure;

[ExcludeFromCodeCoverage]
public static class Setup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IExemploRabbitRepository, ExemploRabbitRepository>();

        services.AddRabbitMq(); 
        return services;
    }
}