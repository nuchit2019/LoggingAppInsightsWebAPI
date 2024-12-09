
# LoggingAppInsightsWebAPI

This project demonstrates how to implement structured logging in an ASP.NET Core Web API application using Application Insights and custom logging logic. 

---

## Table of Contents

1. [Features](#features)  
2. [Technologies Used](#technologies-used)  
3. [Getting Started](#getting-started)  
4. [Configuration](#configuration)  
5. [Usage](#usage)  
6. [Custom Logging Messages](#custom-logging-messages)  
7. [Custom Log Filtering](#custom-log-filtering)  

---

## Features

- Integration with **Azure Application Insights** for telemetry and logging.
- Custom logging messages for consistent log formatting.
- Log filtering to exclude unnecessary or sensitive messages.
- Structured logging for improved log readability.
- Exception handling and detailed error logging.

---

## Technologies Used

- **ASP.NET Core** (Web API)
- **Application Insights** (Azure Telemetry)
- **Microsoft.Extensions.Logging**
- **System.Text.Json** (for data serialization)
- **C# 10**

---

## Getting Started

### Prerequisites

1. .NET SDK (6.0 or later) installed.
2. Azure Application Insights resource set up in your Azure portal.
3. Application Insights connection string.

### Installation

1. Clone the repository:  
   ```bash
   git clone https://github.com/your-repo/LoggingAppInsights.git
   cd LoggingAppInsights
   ```

2. Install dependencies:  
   ```bash       
   dotnet add package Microsoft.Extensions.Logging.ApplicationInsights --version 2.22.0
   ```


3. Configure Application Insights (see [Configuration](#configuration)).

4. Run the application:  
   ```bash
   dotnet run
   ```

5. Access the Swagger UI:  
   Navigate to `https://localhost:5001/swagger` to explore the API endpoints.

---

## Configuration

### Application Insights

In the `appsettings.json` file, add the `APPLICATIONINSIGHTS_CONNECTION_STRING` in the `ConnectionStrings` section:

```json
{
  "ConnectionStrings": {
    "APPLICATIONINSIGHTS_CONNECTION_STRING": "your-connection-string-here"
  },
  "ApplicationLogName": "WeatherApp"
}
```

### Logging Customization

- **Application Name**: Defined in `appsettings.json` as `ApplicationLogName`.
- **Excluded Messages**: Customizable in `CustomLogFilter`.

---

## Usage

### WeatherForecast API

- **Endpoint**: `/WeatherForecast`
- **Method**: `GET`
- **Description**: Returns a list of randomly generated weather forecasts.

---

## Custom Logging Messages

The `LoggingMessages` class provides predefined message templates for consistent logging:

- **START_PROCESS**: Logs the start of a process.  
- **WARNING_PROCESS**: Logs a warning during a process.  
- **SUCCESS_PROCESS**: Logs the successful completion of a process.  
- **EXCEPTION_PROCESS**: Logs an exception with detailed information.  

Example:
```csharp
_logger.LogInformation(LoggingMessages.START_PROCESS + "WeatherForecast Request started.");
```

---

## Custom Log Filtering

The `CustomLogFilter` class excludes specific log messages to avoid clutter. Update the `ExcludedMessages` array to modify the filter:

```csharp
private static readonly string[] ExcludedMessages = new[]
{
    "Content root path:",
    "Hosting environment:",
    "Application started."
};
```

---

## Contact

For questions or support, please contact:  
**Nuchit Atjanawat**  
**Email**: nuchit@outlook.com  
**GitHub**: [nuchit2019](https://github.com/nuchit2019)

--- 
 
