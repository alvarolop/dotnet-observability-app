using Prometheus;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HelloWorldApp.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add controller services to the application's service collection.
builder.Services.AddControllers();

builder.Services.AddHealthChecks();

var app = builder.Build();

// Enable routing for the application.
app.UseRouting();

// Maps the controllers to endpoints based on their routes.
app.MapControllers();

app.UseHttpMetrics();

app.MapHealthChecks("/health/live");

app.MapHealthChecks("/health/ready");

app.MapMetrics();

app.Run("http://*:8080");