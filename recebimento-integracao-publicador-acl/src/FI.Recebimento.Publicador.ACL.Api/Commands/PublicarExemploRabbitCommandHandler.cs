using System;
using MassTransit;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using MediatR;
using Localiza.BuildingBlocks.Logging.Models;
using Localiza.BuildingBlocks.Logging.Services.Interfaces;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;
using BuildingBlock.CorrelationId.Services.Interfaces;
using FI.Recebimento.Publicador.ACL.Api.Infrastructure.RabbitMq.Extensions;
using FI.Recebimento.Publicador.ACL.Domain.AggregatesModel;

namespace FI.Recebimento.Publicador.ACL.Api.Commands;

public class PublicarExemploRabbitCommandHandler : IRequestHandler<PublicarExemploRabbitCommand>
{
    private readonly ILocalizaLabsLogger logger;
    private readonly IPublishEndpoint publishExemploRabbit;
    private readonly ICorrelationIdGeneratorService correlationIdGeneratorService;

    public PublicarExemploRabbitCommandHandler(ILocalizaLabsLogger logger,
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