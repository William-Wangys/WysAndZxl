using DI_04.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DI_04.Service
{
    public class Foo : Base, IFoo, IDisposable { }
    public class Bar : Base, IBar, IDisposable { }
    public class Baz : Base, IBaz, IDisposable { }

    public class Foobar<T1, T2> : IFoobar<T1, T2>
    {
        public IFoo Foo { get; }

        public IBaz Baz { get; }

        public Foobar(IFoo foo, IBaz baz)
        {
            Foo = foo;
            Baz = baz;
        }
    }
}
