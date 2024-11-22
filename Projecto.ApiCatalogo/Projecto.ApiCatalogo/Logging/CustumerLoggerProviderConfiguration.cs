namespace Projecto.ApiCatalogo.Logging;

public class CustumerLoggerProviderConfiguration
{
    public LogLevel LogLevel { get; set; } = LogLevel.Warning;
    public int EventId { get; set; } = 0; 
}