namespace LoggingAppInsights.Logging
{
    public class CustomLogFilter
    {
        private static readonly string[] ExcludedMessages = new[]
        {
            "Content root path:",
            "Hosting environment:",
            "Application started.",
            "Now listening on:",
            "The ASP.NET Core developer certificate is not trusted.",
            "Settings have been set from Engine",
            "Settings contract has been updated",
            "Enabled has been set to True"
        };

        public static bool Filter(string category, LogLevel logLevel, EventId eventId, string message, Exception exception)
        {
            if (message == null) return true;

            foreach (var excludedMessage in ExcludedMessages)
            {
                if (message.Contains(excludedMessage))
                {
                    return false;
                }
            }

            return true;
        }
    }
}