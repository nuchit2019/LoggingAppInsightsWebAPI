
# LoggingAppInsightsWebAPI

This project demonstrates how to implement structured logging in an ASP.NET Core Web API application using Application Insights and custom logging logic. 

---
![image](https://github.com/user-attachments/assets/c91f1e5f-c618-4827-88a1-368f51a329e2)


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

![image](https://github.com/user-attachments/assets/1a85c269-a2f1-47d2-a062-7118cea8d072)

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

### Explanation of the Code Line:

```csharp
builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>(LoggingMessages.FILTER_PROCESS, LogLevel.Trace);
```

#### Purpose:
This line configures a logging filter for the **Application Insights Logger** (`ApplicationInsightsLoggerProvider`). It applies a filter based on the logging category and the minimum log level. Here's a breakdown:

---

### Components:

1. **`builder.Logging`**:
   - Refers to the logging configuration being set up in the application.

2. **`AddFilter`**:
   - Adds a filter to the logging pipeline. Filters are used to restrict or modify what log entries are processed by a specific provider.

3. **`ApplicationInsightsLoggerProvider`**:
   - The logging provider for **Azure Application Insights**.
   - Logs are sent to Application Insights for telemetry and analysis.

4. **`LoggingMessages.FILTER_PROCESS`**:
   - This specifies the **logging category** to target.
   - In your code, `LoggingMessages.FILTER_PROCESS` is defined as:
     ```csharp
     public static string FILTER_PROCESS => $"{ApplicationName} Process";
     ```
   - It dynamically includes the application's name (configured in `appsettings.json` or defaults to `"DefaultApp"`) into the logging category.

5. **`LogLevel.Trace`**:
   - Specifies the **minimum logging level** for this filter.
   - `LogLevel.Trace` is the most detailed log level, capturing all log messages, including highly verbose diagnostic information.

---

### Behavior:
- This filter ensures that **only logs with the category matching `LoggingMessages.FILTER_PROCESS`** and **log levels of `Trace` or higher** (e.g., `Debug`, `Information`, `Warning`, `Error`, `Critical`) are passed to the Application Insights Logger.
- Any log entries with a different category or lower log levels will be ignored by this logger.

---

### Example in Context:

If `ApplicationLogName` in your `appsettings.json` is set to `"WeatherApp"`, the filter will target the logging category **`"WeatherApp Process"`**:

```csharp
builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("WeatherApp Process", LogLevel.Trace);
```

- All logs with this category (e.g., `"WeatherApp Process"`) and `LogLevel.Trace` or higher will be sent to Application Insights.

---

### Use Case:

This filtering allows for:
- **Fine-grained control**: Only specific logs are sent to Application Insights, reducing noise and cost.
- **Custom targeting**: Focus on logs relevant to specific processes, categories, or parts of your application.

For example:
- Logs related to request handling in the `WeatherForecastController` can have the category `"WeatherApp Process"`.
- Other logs (e.g., framework logs, other controllers) can be excluded or processed differently.

Example:
```csharp
_logger.LogInformation(LoggingMessages.START_PROCESS + "WeatherForecast Request started.");
```

---

## Custom Log Filtering excludes specific log messages

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
 
