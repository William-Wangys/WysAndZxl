using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace DiagnosticsLogging8
{
    public class DatabaseSourceListener : EventListener
    {
        protected override void OnEventSourceCreated(EventSource eventSource) 
        {
            if (eventSource.Name == "Artech-Data-SqlClient")
            {
                
            }
        }
    }
}
