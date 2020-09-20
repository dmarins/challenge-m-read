using System;

namespace M.Challenge.Read.Domain.Logger
{
    public interface ILogger
    {
        void Info(string message, params object[] propertyValues);
        void Error(string message, Exception exception, params object[] propertyValues);
        void Warning(string message, Exception exception = null, params object[] propertyValues);
    }
}
