using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System;
using System.Diagnostics;

namespace HelloWorldApp.Controllers
{
    [Controller]
    public class HomeController : ControllerBase
    {
        // Create Prometheus metrics
        private static readonly Counter HelloCounter = Metrics.CreateCounter("app_hello_counter_total", "Total requests to /hello endpoint");
        private static readonly Histogram HelloHistogram = Metrics.CreateHistogram("app_hello_request_duration_seconds", "Request duration for /hello endpoint");

        [HttpGet("/hello")]
        public IActionResult GetHello()
        {
            // Increment the counter metric
            HelloCounter.Inc();

            // Measure the request duration
            using (HelloHistogram.NewTimer())
            {
                string greetingsName = Environment.GetEnvironmentVariable("GREETINGS_NAME") ?? "Alvaro";
                return Ok($"Welcome to our .NET application, {greetingsName}!");
            }
        }

        [HttpGet("/error")]
        public IActionResult GetError()
        {
            try
            {
                throw new Exception("An error occurred!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
