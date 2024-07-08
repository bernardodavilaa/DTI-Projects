namespace FI.Recebimento.Publicador.ACL.Api.Controllers;

[ApiController]
[Route("api/v1/weatherforecast")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly Logger logger;

    public WeatherForecastController(ILogger logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var lista = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });

        foreach (var item in lista)
        {
            logger.Log(LogLevel.Debug, "Weather Forecast - {Summary}", 
                new List<ParametersLogModel> 
                {
                    new (item.Summary, ObfuscateTypeForSensitiveData.Half) // Exemplo de obfuscação de dado sensível
                },
                flow: nameof(WeatherForecastController));
        }

        return Ok(lista);
    }
}