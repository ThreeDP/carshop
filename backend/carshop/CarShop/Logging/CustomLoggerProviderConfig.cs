namespace CarShop.Logger;

public class CustomLoggerProviderConfig {
    public LogLevel LogLevel { get; set; } = LogLevel.Warning;
    public int EventId { get; set; } = 0;
}