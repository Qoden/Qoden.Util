using Microsoft.Extensions.Logging;
using SerilogLogger = Serilog.ILogger;

namespace Qoden.Serilog
{
    public static class SerilogExtensions
    {
        public static ILoggerFactory AddSerilogProvider(this ILoggerFactory factory, SerilogLogger logger = null)
        {            
            factory.AddProvider(new SerilogLoggerProvider(logger));
            return factory;
        }
    }
}