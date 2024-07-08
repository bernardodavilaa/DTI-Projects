using System;
using MassTransit;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;
using FI.Recebimento.Publicador.ACL.Api.Infrastructure.RabbitMq.Extensions;
using FI.Recebimento.Publicador.ACL.Domain.AggregatesModel;

namespace FI.Recebimento.Publicador.ACL.Api.Commands;

public class PublicarExemploRabbitCommandHandler : IRequestHandler<PublicarExemploRabbitCommand>
{
    private readonly Logger logger;
    private readonly IPublishEndpoint publishExemploRabbit;
    private readonly ICorrelationIdGeneratorService correlationIdGeneratorService;

    public PublicarExemploRabbitCommandHandler(ILogger logger,
        IPublishEndpoint publishExemploRabbit,
        ICorrelationIdGeneratorService correlationIdGeneratorService)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.publishExemploRabbit = publishExemploRabbit ?? throw new ArgumentNullException(nameof(publishExemploRabbit));
        this.correlationIdGeneratorService = correlationIdGeneratorService ?? throw new ArgumentNullException(nameof(correlationIdGeneratorService));
    }

    public async Task Handle(PublicarExemploRabbitCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var message = new ExemploRabbit(Guid.NewGuid(), command.Nome);

            await publishExemploRabbit.Publish(message, correlationIdGeneratorService.CorrelationId);

            logger.Log(LogLevel.Debug, "Mensagem publicada sucesso!",
                new List<ParametersLogModel>
                {
                    new(command.Nome)
                },
                flow: nameof(PublicarExemploRabbitCommandHandler));
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, e, "Falha na entrega da mensagem.",
                new List<ParametersLogModel>
                {
                    new(command.Nome)
                },
                flow: nameof(PublicarExemploRabbitCommandHandler));
            throw;
        }
        
    }
}