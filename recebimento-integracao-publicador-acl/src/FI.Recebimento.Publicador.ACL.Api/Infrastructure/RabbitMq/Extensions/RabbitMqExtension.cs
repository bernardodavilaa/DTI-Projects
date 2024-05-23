using MassTransit;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace FI.Recebimento.Publicador.ACL.Api.Infrastructure.RabbitMq.Extensions;

[ExcludeFromCodeCoverage]
public static class RabbitMqExtension
{
    public static async Task Publish<T>(this IPublishEndpoint publishEndpoint, T message, Guid correlationId, TimeSpan timeout, [CallerMemberName] string memberName = "") where T : class
    {
        var cancellationToken = new CancellationTokenSource(timeout).Token; 
        
        await Publish(publishEndpoint, message, correlationId, cancellationToken, memberName);
    }

    public static async Task Publish<T>(this IPublishEndpoint publishEndpoint, T message, Guid correlationId, CancellationToken cancellationToken = default, [CallerMemberName] string memberName = "") where T : class
        => await publishEndpoint.Publish(message, context => context.CorrelationId = correlationId, cancellationToken);
}