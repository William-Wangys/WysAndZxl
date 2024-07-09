using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    /// <summary>
    /// 构造函数注入
    /// </summary>
    public class Foo
    {
        public IBar _bar { get; }
        public Foo(IBar bar) => _bar = bar;
    }
}
