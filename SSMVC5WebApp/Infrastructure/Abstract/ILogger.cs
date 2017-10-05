using System;

namespace SSMVC5WebApp.Infrastructure.Abstract
{
    public interface ILogger
    {
        void Information(string message);
        void Information(string stringFormat, params object[] values);
        void Information(Exception exception, string stringFormat, params object[] values);


        void Warning(string message);
        void Warning(string stringFormat, params object[] values);
        void Warning(Exception exception, string stringFormat, params object[] values);

        void Error(string message);
        void Error(string stringFormat, params object[] values);
        void Error(Exception exception, string stringFormat, params object[] values);

        void TraceApi(string componentName, string method);
        void TraceApi(string componentName, string method, string properties);
        void TraceApi(string componentName, string method, string stringFormat, params object[] values);
        void TraceApi(string componentName, string method, TimeSpan timeSpan);
        void TraceApi(string componentName, string method, TimeSpan timeSpan, string properties);
        void TraceApi(string componentName, string method, TimeSpan timeSpan, string stringFormat, params object[] values);
    }
}
