using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.SeriLogs.ColumnWriters;

public class UserNameColumnWriter : ColumnWriterBase
{
    public UserNameColumnWriter() : base(NpgsqlTypes.NpgsqlDbType.Varchar){ }
    public override object GetValue(LogEvent logEvent, IFormatProvider? formatProvider = null)
    {
        var (userName, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");
        return value?.ToString() ?? string.Empty;
    }
}
