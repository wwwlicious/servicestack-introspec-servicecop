// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.Logging;

    public static class LoggingExtensions
    {
        public static IStructuredLog GetStructuredLog(this ILogFactory lf)
        { 
            return new SerilogStructuredLogger(Serilog.Log.Logger);
        }

        public static IStructuredLog With(this IStructuredLog logger, string name, object property, bool destructureObjects = false)
        {
            return logger.ForContext(name, property, destructureObjects);
        }
    }
}