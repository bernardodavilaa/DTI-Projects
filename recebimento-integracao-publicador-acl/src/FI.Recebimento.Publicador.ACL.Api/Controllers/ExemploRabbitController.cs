using FI.Recebimento.Publicador.ACL.Api.Commands; 
using Localiza.BuildingBlocks.Logging.Models;
using Localiza.BuildingBlocks.Logging.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.ComponentModel.DataAnnotations;
using FI.Recebimento.Publicador.ACL.Domain.AggregatesModel;

namespace FI.Recebimento.Publicador.ACL.Api.Controllers;

/// <summary>
/// Controlador para lidar com operações relacionadas a ExemploRabbit.
/// </summary>
[Route("[controller]")]
[ApiController]
public class ExemploRabbitController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IExemploRabbitRepository exemplorabbitRepository;
    private readonly ILocalizaLabsLogger logger;

    /// <summary>
    /// Inicializa uma nova instância da classe ExemploRabbitController.
    /// </summary>
    /// <param name="logger">O serviço de registro de logs.</param>
    /// <param name="mediator">O serviço do mediador para comandos e consultas.</param>
    /// <param name="exemplorabbitRepository">O repositório da entidade ExemploRabbit.</param>
    public ExemploRabbitController(ILocalizaLabsLogger logger, IMediator mediator, IExemploRabbitRepository exemplorabbitRepository)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.exemplorabbitRepository = exemplorabbitRepository ?? throw new ArgumentNullException(nameof(exemplorabbitRepository));
    }

    [HttpPost("publish")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity, Type = typeof(ErrorModel))]
    public async Task<ActionResult<Guid>> Publish([FromBody] PublicarExemploRabbitCommand command, CancellationToken token)
    {
        logger.Log(LogLevel.Debug, "Enviando mensagem...", new List<ParametersLogModel>
        {
            new(command.Nome)
        }, flow: nameof(PublicarExemploRabbitCommand));

        await mediator.Send(command, token);

        return Accepted();
    }

    
}