using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Logging
{
    public class LoggingAppAfpaBrive
    {
        public bool AutoLog { get; set; }
        
        public string CreateEventSource()
        {
            string eventSource = "AppAfpaBrive";
            bool sourceExists;
            bool logExists;
            try
            {
                
                // searching the source throws a security exception ONLY if not exists!
                sourceExists = EventLog.SourceExists(eventSource);
                logExists = EventLog.Exists("LogAgpa");
                this.AutoLog = false;
                if (!sourceExists)
                {   // no exception until yet means the user as admin privilege
                    //EventLog.DeleteEventSource("AppAfpaBrive");
                    EventLog.CreateEventSource(eventSource, "LogAfpa");  
                }
            }
            catch (SecurityException)
            {
                eventSource = "Application";
            }
            return eventSource;
        }


    }
}
