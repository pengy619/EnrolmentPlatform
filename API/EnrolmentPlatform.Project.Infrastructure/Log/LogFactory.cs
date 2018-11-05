using log4net;
using System;
using System.IO;
using System.Web;

namespace EnrolmentPlatform.Project.Infrastructure
{
    public class LogFactory
    {
        static LogFactory()
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo configFile = new FileInfo(Path.Combine(currentDir, "Configs\\Log4net.config"));
            log4net.Config.XmlConfigurator.Configure(configFile);
        }
        public static Log GetLogger(Type type)
        {
            return new Log(LogManager.GetLogger(type));
        }
        public static Log GetLogger(string str)
        {
            return new Log(LogManager.GetLogger(str)); 
        }
    }
}
