using System;
using System.Collections.Generic;
using System.Text;

namespace DI_04.Service
{
    public class Base : IDisposable
    {
        public Base() => Console.WriteLine($"创建一个{GetType().Name}实例");
        public void Dispose() => Console.WriteLine($"释放一个{GetType().Name}实例");
    }
}
