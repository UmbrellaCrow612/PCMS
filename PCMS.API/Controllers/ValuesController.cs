using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/weatherforecast")]
    [ApiVersion(1)]
    [ApiVersion(2)]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetV1()
        {
            return Ok(new { Version = "1.0", Weather = "Sunny" });
        }

        [HttpGet, MapToApiVersion("2.0")]
        public IActionResult GetV2()
        {
            return Ok(new { Version = "2.0", Weather = "Cloudy" });
        }
    }
}
