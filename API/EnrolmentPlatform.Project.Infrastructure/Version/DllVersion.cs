using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.Version
{
    public class DllVersion
    {
        private readonly static string _DllVersion;
        private readonly static DateTime _DllTime;
        static DllVersion()
        {
            _DllVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            _DllTime=System.IO.File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location);
        }
        public static string Version
        {
            get
            {
                return _DllVersion;
            }
        }
        public static DateTime DllTime
        {
            get
            {
                return _DllTime;
            }
        }
    }
}
