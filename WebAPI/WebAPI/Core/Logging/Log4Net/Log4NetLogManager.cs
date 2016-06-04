using System;

namespace WebAPI.Core.Logging.Log4Net
{
    public class Log4NetLogManager : ILogManager
    {
        static Log4NetLogManager()
        {
            // Load configurations from an xml configuration file
            //log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("Log4Net.config"));
            
        }
        public ILogging GetLogger(Type type)
        {
            var logger = log4net.LogManager.GetLogger(type);
            return new Log4NetLoggingAdapter(logger);
        }
    }
}
