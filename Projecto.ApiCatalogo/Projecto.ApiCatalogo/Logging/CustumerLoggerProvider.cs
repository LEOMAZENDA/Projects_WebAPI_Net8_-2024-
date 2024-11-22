using System.Collections.Concurrent;

namespace Projecto.ApiCatalogo.Logging;

public class CustumerLoggerProvider : ILoggerProvider
{
    readonly CustumerLoggerProviderConfiguration loggerConfig;

    readonly ConcurrentDictionary<string, CustumerLogger> Loggers =
                          new ConcurrentDictionary<string, CustumerLogger>();

    public CustumerLoggerProvider(CustumerLoggerProviderConfiguration config)
    {
        loggerConfig = config;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return Loggers.GetOrAdd(categoryName, name => new CustumerLogger(name, loggerConfig));
    }
    
    public void Dispose()
    {
        // TODO release managed resources here
        Loggers.Clear();
    }

}