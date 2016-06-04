using System;

namespace WebAPI.Core.Logging
{
    public interface ILogManager
    {
        ILogging GetLogger(Type type);
    }
}
