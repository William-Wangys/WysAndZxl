using DI_04.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DI_04.Service
{
    public class Foo: IFoo 
    {
        public IBar _bar { get; }

        public Foo(IBar bar) => _bar = bar;
    }
    public class Bar : IBar 
    {

    }
    public class Baz : Base, IBaz, IDisposable { }

    public class Foobar : IFoobar
    {
        private Foobar() { }

        public static readonly Foobar Instance = new Foobar();
    }
}
