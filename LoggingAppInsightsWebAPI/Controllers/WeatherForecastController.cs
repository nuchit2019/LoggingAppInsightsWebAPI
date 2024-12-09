using LoggingAppInsights.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LoggingAppInsights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
         


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger; // Dependency injection for logging

        // Constructor initializes the logger
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var model = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();
            var modelString = JsonSerializer.Serialize(model);

            var processName = nameof(Get);

            //=====================================================//
            // 1. START_PROCESS
            //=====================================================//
            _logger.LogInformation(LoggingMessages.START_PROCESS + processName + " Request data: {Request}", modelString);

            try
            {

                // ..............................
                //*** Validation Logic
                // ..............................
                //=====================================================//
                // 2. WARNING_PROCESS
                //=====================================================//
                _logger.LogWarning(LoggingMessages.WARNING_PROCESS + processName);


                // ..............................
                //*** Business logic
                // ..............................
                //=====================================================//
                // 3. SUCCESS_PROCESS
                //=====================================================//
                _logger.LogInformation(LoggingMessages.SUCCESS_PROCESS + processName );

                    

                // Test exception
                throw new Exception(LoggingMessages.START_PROCESS+ processName +" Test exception for logging.");             
                
            }
            catch(Exception ex)
            {
                //=====================================================//
                // 4. EXCEPTION_PROCESS
                //=====================================================//

                var errorData = new
                {
                    ExceptionMessage = ex.Message,
                    FileName = new System.Diagnostics.StackTrace(ex, true).GetFrame(0)?.GetFileName(),
                    LineNumber = new System.Diagnostics.StackTrace(ex, true).GetFrame(0)?.GetFileLineNumber().ToString()
                };
                var errorDataString = JsonSerializer.Serialize(errorData);

                _logger.LogError(LoggingMessages.EXCEPTION_PROCESS + processName + " LogError data: {LogError}", errorDataString, ex);
            }

            return model;
        }
    }
}
