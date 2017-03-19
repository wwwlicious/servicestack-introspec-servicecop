namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using System;
    using Serilog;
    using ServiceStack.Extras.Serilog;
    using ServiceStack.Logging;

    public class SerilogStructuredLogger : SerilogLogger, IStructuredLog
    {
        private readonly ILogger log;

        public SerilogStructuredLogger(ILogger log) : base(log)
        {
            this.log = log;
        }

        public void Debug(string message, params object[] propertyValues)
        {
            log.Debug(message, propertyValues);
        }

        public void Debug(Exception exception, string message, params object[] propertyValues)
        {
            log.Debug(exception, message, propertyValues);
        }

        public void Info(Exception exception, string message, params object[] propertyValues)
        {
            log.Information(exception, message, propertyValues);
        }

        public void Warn(Exception exception, string message, params object[] propertyValues)
        {
            log.Warning(exception, message, propertyValues);
        }

        public void Error(Exception exception, string message, params object[] propertyValues)
        {
            log.Error(exception, message, propertyValues);
        }

        public void Fatal(Exception exception, string message, params object[] propertyValues)
        {
            log.Fatal(exception, message, propertyValues);
        }

        public void Info(string message, params object[] propertyValues)
        {
            log.Information(message, propertyValues);
        }

        public void Warn(string message, params object[] propertyValues)
        {
            log.Warning(message, propertyValues);
        }

        public void Error(string message, params object[] propertyValues)
        {
            log.Error(message, propertyValues);
        }

        public void Fatal(string message, params object[] propertyValues)
        {
            log.Fatal(message, propertyValues);
        }

        public IStructuredLog ForContext(string propertyName, object value, bool destructureObjects = false)
        {
            return LogManager.LogFactory.GetStructuredLog().ForContext(propertyName, value, destructureObjects);
        }

        public IStructuredLog ForContext<TSource>()
        {
            return LogManager.LogFactory.GetStructuredLog().ForContext<TSource>();
        }

        public IStructuredLog ForContext(Type source)
        {
            return LogManager.LogFactory.GetStructuredLog().ForContext(source);
        }
    }
}