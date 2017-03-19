namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using System;
    using ServiceStack.Logging;

    public interface IStructuredLog : ILog
    {
        /// <summary>
        /// Create a logger that enriches log events with the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property. Must be non-empty.</param>
        /// <param name="value">The property value.</param>
        /// <param name="destructureObjects">If true, the value will be serialized as a structured
        /// object if possible; if false, the object will be recorded as a scalar or simple array.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        IStructuredLog ForContext(string propertyName, object value, bool destructureObjects = false);

        /// <summary>
        /// Create a logger that marks log events as being from the specified
        /// source type.
        /// </summary>
        /// <typeparam name="TSource">Type generating log messages in the context.</typeparam>
        /// <returns>A logger that will enrich log events as specified.</returns>
        IStructuredLog ForContext<TSource>();

        /// <summary>
        /// Create a logger that marks log events as being from the specified
        /// source type.
        /// </summary>
        /// <param name="source">Type generating log messages in the context.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        IStructuredLog ForContext(Type source);

        void Debug(string message, params object[] propertyValues);
        void Debug(Exception exception, string message, params object[] propertyValues);
        void Info(string message, params object[] propertyValues);
        void Info(Exception exception, string message, params object[] propertyValues);
        void Warn(string message, params object[] propertyValues);
        void Warn(Exception exception, string message, params object[] propertyValues);
        void Error(string message, params object[] propertyValues);
        void Error(Exception exception, string message, params object[] propertyValues);
        void Fatal(string message, params object[] propertyValues);
        void Fatal(Exception exception, string message, params object[] propertyValues);
    }
}