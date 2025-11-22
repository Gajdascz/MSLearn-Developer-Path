namespace M6_Create_A_Web_API_With_ASPNET_Core_Controllers;

public class WeatherForecast
{
  public DateOnly Date { get; set; }

  public int TemperatureC { get; set; }

  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

  public string? Summary { get; set; }
}
