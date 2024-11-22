namespace Projecto.ApiCatalogo.Logging;

public class CustumerLogger : ILogger
{
    readonly string koogerName; 
    readonly CustumerLoggerProviderConfiguration loggerConfig;

    public CustumerLogger(string name, CustumerLoggerProviderConfiguration config)
    {
        koogerName = name;
        loggerConfig = config;
    }

    public IDisposable? BeginScope<TState>(TState state) 
    {
        return null;
    }
    
    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, 
        Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

        EscreverTextoNoArquivo(mensagem);
    }
    private void EscreverTextoNoArquivo(string mensagem)
    {
        string caminhoArqivoLog = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
            "log_api", 
            "mazenda_apiLog.txt");

     
        var diretorio = Path.GetDirectoryName(caminhoArqivoLog);
        if (!Directory.Exists(diretorio))
            Directory.CreateDirectory(diretorio);
    
        using (StreamWriter streamWriter = new StreamWriter(caminhoArqivoLog, true))
        {
            try
            {
                streamWriter.WriteLine($"{DateTime.Now}: {mensagem}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

}