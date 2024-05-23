namespace FI.Recebimento.Publicador.ACL;

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.Half)]
    public string? Summary { get; set; }
}