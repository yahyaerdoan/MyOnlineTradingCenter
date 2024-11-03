using MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.SeriLogs.ColumnWriters;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Extensions;

public static class SeriLoggerConfigurationServiceRegistration
{
    public static void AddLoggerConfigurationServiceRegistrations(this IServiceCollection services, IConfiguration configurations)
    {

        var seqServerUrl = configurations["SeqLoggerUserInterface:SeqServerUrl"];

        if (string.IsNullOrEmpty(seqServerUrl))
        {
            throw new ArgumentException("Seq server URL is not configured correctly.");
        }

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configurations)
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt")
            .WriteTo.PostgreSQL(configurations.GetConnectionString("PostgreSql"), "Logs", needAutoCreateTable: true, columnOptions: new Dictionary<string, ColumnWriterBase>{
                {"message", new RenderedMessageColumnWriter() },
                {"message_template", new MessageTemplateColumnWriter() },
                {"time_stamp", new TimestampColumnWriter() },
                {"exception", new ExceptionColumnWriter() },
                {"log_event", new LogEventSerializedColumnWriter() },
                {"user_name", new UserNameColumnWriter() }})
            .WriteTo.Seq(seqServerUrl)
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .CreateLogger();

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
    }
}
