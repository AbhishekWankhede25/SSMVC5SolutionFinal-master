using System;

using SSMVC5WebApp.Infrastructure.Abstract;
using System.Diagnostics;

namespace SSMVC5WebApp.Infrastructure.Concrete
{
    public class Logger : ILogger
    {
        #region ILogger Members

        #region Warning - trace information within the application
            public void Information(string message)
            {
                Trace.TraceInformation(message);
            }

            public void Information(string stringFormat, params object[] values)
            {
                Trace.TraceInformation(stringFormat, values);
            }

            public void Information(Exception exception, string stringFormat, params object[] values)
            {
                //var msg = string.Format(stringFormat, values);
                Trace.TraceInformation(string.Format(stringFormat, values) + ";Exception Details: {0}", ExceptionUtilities.FormatException(exception, includeContext: true));
            }
        #endregion

        #region Warning - trace warnings within the application
            public void Warning(string message)
            {
                Trace.TraceWarning(message);
            }

            public void Warning(string stringFormat, params object[] values)
            {
                Trace.TraceWarning(stringFormat, values);
            }

            public void Warning(Exception exception, string stringFormat, params object[] values)
            {
                Trace.TraceWarning(string.Format(stringFormat, values) + ";Exception Details: {0}", ExceptionUtilities.FormatException(exception, includeContext: true));
            }
        #endregion

        #region Error - trace fatal errors within the application
            public void Error(string message)
            {
                Trace.TraceError(message);
            }

            public void Error(string stringFormat, params object[] values)
            {
                Trace.TraceError(stringFormat, values);
            }

            public void Error(Exception exception, string stringFormat, params object[] values)
            {
                Trace.TraceError(string.Format(stringFormat, values) + ";Exception Details: {0}", ExceptionUtilities.FormatException(exception, includeContext: true));
            }
        #endregion

        #region TraceAPI - trace inter-service calls (including latency)
        // TraceAPI - trace inter-service calls
            public void TraceApi(string componentName, string method)
            {
                TraceApi(componentName, method, "");
            }

            public void TraceApi(string componentName, string method, string properties)
            {
                string message = String.Concat("component:", componentName, ";method:", method, ";properties:", properties);
                Trace.TraceInformation(message);
            }

            public void TraceApi(string componentName, string method, string stringFormat, params object[] values)
            {
                TraceApi(componentName, method, string.Format(stringFormat, values));
            }

            // TraceAPI - trace inter-service calls (including latency)
            public void TraceApi(string componentName, string method, TimeSpan timeSpan)
            {
                TraceApi(componentName, method, timeSpan, "");
            }

            public void TraceApi(string componentName, string method, TimeSpan timeSpan, string properties)
            {
                string message = String.Concat("component:", componentName, ";method:", method, ";timespan:", timeSpan.ToString(), ";properties:", properties);
                Trace.TraceInformation(message);
            }
            public void TraceApi(string componentName, string method, TimeSpan timeSpan, string stringFormat, params object[] values)
            {
                TraceApi(componentName, method, timeSpan, string.Format(stringFormat, values));
            }
        #endregion

        #endregion
    }
}