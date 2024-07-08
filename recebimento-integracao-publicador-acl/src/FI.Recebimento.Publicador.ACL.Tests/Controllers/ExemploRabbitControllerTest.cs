using FI.Recebimento.Publicador.ACL.Api.Commands; 
using .BuildingBlocks.Logging.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FI.Recebimento.Publicador.ACL.Api.Controllers;
using FI.Recebimento.Publicador.ACL.Domain.AggregatesModel;
using Xunit;

namespace FI.Recebimento.Publicador.ACL.Tests.Controllers;

/// <summary>
/// Classe de teste para o controlador ExemploRabbitController.
/// </summary>
public class ExemploRabbitControllerTest
{
    private readonly Mock<IExemploRabbitRepository> exemplorabbitRepositoryMock;
    private readonly Mock<ILogger> loggerMock;
    private readonly Mock<IMediator> mediatorMock;
    private readonly ExemploRabbitController controller;

    /// <summary>
    /// Inicializa uma nova instância da classe de teste ExemploRabbitControllerTest.
    /// </summary>
    public ExemploRabbitControllerTest()
    {
        exemplorabbitRepositoryMock = new Mock<IExemploRabbitRepository>();
        loggerMock = new Mock<ILogger>();
        mediatorMock = new Mock<IMediator>();
        controller = new ExemploRabbitController(loggerMock.Object, mediatorMock.Object, exemplorabbitRepositoryMock.Object);
    }
    




    [Fact]
    public async Task PublishExemploRabbit_QuandoValido_DeveRetornarCreated()
    {
        // Arrange
              
        var command = new PublicarExemploRabbitCommand("ABC1234");

        // Act
        var result = await controller.Publish(command, CancellationToken.None);

        // Assert
        Assert.IsType<AcceptedResult>(result.Result);
    }


}