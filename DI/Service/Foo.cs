using DI.Attributes;
using DI.Interface;
using System.Threading.Tasks;

namespace DI.Service
{
    public class Foo: Base, IFoo
    {
        public IBar _bar { get; private set; }

        //属性注入
        [Injection]
        public IBaz _baz { get; private set; }

        //构造函数注入
        [Injection]
        public Foo(IBar bar) => _bar = bar;

        public Foo(IBar bar, IBaz baz) : this(bar) => _baz = baz;

        public async Task InvokeAsync() 
        {
            await _bar.InvokeAsync();
            await _baz.InvokeAsync();
        }
    }
}
