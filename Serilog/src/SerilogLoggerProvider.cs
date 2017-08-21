using System;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using SerilogLogger = Serilog.ILogger;

namespace Qoden.Serilog
{
    public class SerilogLoggerProvider : ILoggerProvider
    {
        private readonly SerilogLogger _logger;
        private readonly bool _dispose;

        public SerilogLoggerProvider(SerilogLogger logger = null, bool dispose = false)
        {
            _logger = logger ?? Log.Logger;
            _dispose = dispose;
        }

        public void Dispose()
        {
            if (_dispose)
            {
                if (_logger != null)
                    (_logger as IDisposable)?.Dispose();
                else
                    Log.CloseAndFlush();
            }
        }

        public ILogger CreateLogger(string name)
        {
            return new SerilogLoggerAdapter(_logger.ForContext("SourceContext", name));
        }
    }
}