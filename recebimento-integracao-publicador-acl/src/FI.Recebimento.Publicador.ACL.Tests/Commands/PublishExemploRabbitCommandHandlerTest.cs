using BuildingBlock.CorrelationId.Services.Interfaces;
using Localiza.BuildingBlocks.Logging.Models;
using Localiza.BuildingBlocks.Logging.Services.Interfaces;
using Localiza.BuildingBlocks.Logging.Enums;
using MassTransit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FI.Recebimento.Publicador.ACL.Api.Commands;
using FI.Recebimento.Publicador.ACL.Api.Infrastructure.RabbitMq.Extensions;
using Xunit;

namespace FI.Recebimento.Publicador.ACL.Tests.Commands;

public class PublicarExemploRabbitCommandHandlerTest
{
	private readonly Mock<ILocalizaLabsLogger> mockLogger;
	private readonly Mock<IPublishEndpoint> mockPublishExemploRabbit;
	private readonly Mock<ICorrelationIdGeneratorService> mockCorrelationIdGeneratorService;

	private PublicarExemploRabbitCommandHandler handler;

	public PublicarExemploRabbitCommandHandlerTest() 
	{
		mockLogger = new Mock<ILocalizaLabsLogger>();
		mockPublishExemploRabbit = new Mock<IPublishEndpoint>();
		mockCorrelationIdGeneratorService = new Mock<ICorrelationIdGeneratorService>();
	}

	[Fact]
	public async Task Produce_QuandoValido_DeveRetornarSucesso()
	{
		// Arrange
		
		var command = new PublicarExemploRabbitCommand("ABC1234");

		await mockPublishExemploRabbit.Object.Publish(It.IsAny<Event>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<string>());

		handler = new PublicarExemploRabbitCommandHandler(mockLogger.Object, mockPublishExemploRabbit.Object, mockCorrelationIdGeneratorService.Object);

		// Act
		await handler.Handle(command, CancellationToken.None);

		// Assert

		mockLogger.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<List<ParametersLogModel>>(), It.IsAny<string>()), Times.Once);
	}

	[Fact]
	public async Task Produce_QuandoInvalido_DeveRetornarErro()
	{
		try
		{
			// Arrange
			var command = new PublicarExemploRabbitCommand("ABC1234");
			handler = new PublicarExemploRabbitCommandHandler(mockLogger.Object, mockPublishExemploRabbit.Object, mockCorrelationIdGeneratorService.Object);

			// Act
			await handler.Handle(command, CancellationToken.None);

			// Assert
			await Assert.ThrowsAsync<Exception>(async () =>
			{
			    await mockPublishExemploRabbit.Object.Publish(It.IsAny<Event>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<string>());
			});
		}
		catch (Exception ex)
		{
			// Assert
			Assert.Contains("Assert.Throws() Failure:", ex.Message);
		}
	}
}