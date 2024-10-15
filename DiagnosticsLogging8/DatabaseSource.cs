using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Text;

namespace DiagnosticsLogging8
{
    /// <summary>
    /// 是为某个数据库组件定义的 EventSource
    /// </summary>
    [EventSource(Name = "Artech-Data-SqlClient")]
    public sealed class DatabaseSource : EventSource
    {
        public static readonly DatabaseSource Instance = new DatabaseSource();

        private DatabaseSource() { }

        [Event(1)]
        public void OnCommandExecute(CommandType commandType, string commandText) =>
            WriteEvent(1, commandType, commandText);
    }
}
