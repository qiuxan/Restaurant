using Microsoft.AspNetCore.Mvc;

namespace Restaurant.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
 

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly WeatherForecastService _weatherForecastService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger , WeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var result = _weatherForecastService.Get();
        return result;
    }
}
