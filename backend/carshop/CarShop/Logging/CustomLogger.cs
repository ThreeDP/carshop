namespace CarShop.Logger;

public class CustomLogger : ILogger {
    readonly string loggerName;
    readonly CustomLoggerProviderConfig loggerConfig;

    public CustomLogger(string name, CustomLoggerProviderConfig config) {
        loggerName = name;
        loggerConfig = config;
    }

    public bool IsEnabled(LogLevel logLevel) {
        return logLevel == loggerConfig.LogLevel;
    }

    public IDisposable BeginScope<TState>(TState state) {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception exception, Func<TState, Exception, string> formatter) {
            string msg = $"{logLevel.ToString()}: {eventId} - {formatter(state, exception)}";
            this.WriteLog(msg);
    }

    private void WriteLog(string msg) {
        string path = @"./logs.txt";

        using (StreamWriter fd = new StreamWriter(path, true)) {
            try {
                fd.WriteLine(msg);
                fd.Close();
            } catch (Exception) {
                throw;
            }
        }
    }
}