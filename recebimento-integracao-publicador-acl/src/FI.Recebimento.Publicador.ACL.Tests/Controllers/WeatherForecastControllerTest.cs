using FI.Recebimento.Publicador.ACL.Api.Controllers;
using .BuildingBlocks.Logging.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace FI.Recebimento.Publicador.ACL.Tests.Controllers;

public class WeatherForecastControllerTest
{
    private readonly Mock<ILogger> mockLogger;
    private readonly WeatherForecastController weatherForecastController;

    public WeatherForecastControllerTest()
    {
        this.mockLogger = new Mock<ILogger>();
        this.weatherForecastController = new WeatherForecastController(mockLogger.Object);

        Environment.SetEnvironmentVariable("LOG_LEVEL", "Debug");
    }

    [Fact]
    public void ObterClientePorId_V1_Ok()
    {
        var result = weatherForecastController.Get();

        Assert.IsType<OkObjectResult>(result);
    }
}