using Microsoft.AspNetCore.Mvc;

namespace Restaurant.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{


    private readonly ILogger<WeatherForecastController> _logger;

    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet("{take}/allweather")]
    public ActionResult<WeatherForecast> Get([FromQuery] int max, [FromRoute] int take)
    {
        var result = _weatherForecastService.Get();
        return Ok(result);
    }
}
