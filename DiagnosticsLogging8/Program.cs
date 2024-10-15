using System;
using System.Diagnostics.Tracing;
using System.Diagnostics;
using System.Data;

namespace DiagnosticsLogging8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 8.1 各种诊断日志形式
            //由于写入日志的对象分别为Debugger、TraceSource、EventSource和DiagnosticSource（这些类型都定义在System.Diagnostics命名空间下）
            //所以可以将对应的日志形式称为调试日志、跟踪日志、事件日志和诊断日志。下面对各种日志形式进行简单介绍
            #region 8.1.1 调试日志
            /*
             * Debugger 这个静态类型是.NET Core 托管代码与调试器进行通信的媒介，我们可以利用它启动调试器并将其附加到当前进程上
             * 
             * */
            #region 8.1.2 跟踪日志
            ///*
            // * 
            // * 从设计的角度来讲，接下来介绍的 3 种诊断日志框架采用的都是观察者模式或者发布订阅模式
            // * 
            // */
            //var source = new TraceSource("Foobar", SourceLevels.Warning);
            //source.Listeners.Add(new ConsoleTraceListener());
            //var eventTypes = (TraceEventType[])Enum.GetValues(typeof(TraceEventType));
            //var eventId = 1;
            //Array.ForEach(eventTypes, (t) => source.TraceEvent(t, eventId++, $"This is a {t} message."));
            //Console.Read();


            #endregion
            //EventSource 所谓的强类型编程模式主要体现在如下两个方面：
            //其一，我们需要通过继承抽象类EventSource定义一个具体的EventSource类型，并将发送日志事件的操作实现在它的某个方法中；
            //其二，日志消息的内容可以通过一个自定义的数据类型来承载。下面介绍 EventSource这种强类型的日志记录模式
            #region 8.1.3 事件日志
            var listener = new DatabaseSourceListener();
            DatabaseSource.Instance.OnCommandExecute(CommandType.Text, "SELECT * FROM USER");
            #endregion

            #region 8.1.3 诊断日志

            #endregion
            #endregion
            #endregion
        }
    }



}
