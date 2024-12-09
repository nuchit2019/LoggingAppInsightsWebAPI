using LoggingAppInsights.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

 


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure LoggingConstants
LoggingMessages.Initialize(builder.Configuration);

builder.Logging.AddApplicationInsights(
        configureTelemetryConfiguration: (config) =>
            config.ConnectionString = builder.Configuration.GetConnectionString("APPLICATIONINSIGHTS_CONNECTION_STRING"),
            configureApplicationInsightsLoggerOptions: (options) => { }
    );

// Add filter for Application Insights logging to target specific category 
builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>(LoggingMessages.FILTER_PROCESS, LogLevel.Trace);

// Add custom filter to exclude specific messages
builder.Logging.AddFilter((category, logLevel) => CustomLogFilter.Filter(category, logLevel, default, null, null));

// Add console logging only in development environment
if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddConsole();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();