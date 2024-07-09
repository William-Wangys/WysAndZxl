using DI_04.Interface;
using DI_04.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;

namespace DI_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var provider = new ServiceCollection()
            //    .AddTransient<IFoo, Foo>()
            //    .AddScoped<IBar>(_ => new Bar())
            //    .AddSingleton<IBaz, Baz>()
            //    .BuildServiceProvider();
            //var foobar = (IFoobar<IFoo, IBar>)provider.GetServices<IFoobar<IFoo, IBar>>();
            //Debug.Assert(provider.GetServices<IBar>() is Bar);
            //Debug.Assert(provider.GetServices<IBaz>() is Baz);

            ////创建的ServiceCollection对象添加了3个针对Base类型的服务注册
            //var service = new ServiceCollection()
            //    .AddTransient<Base, Foo>()
            //    .AddTransient<Base, Bar>()
            //    .AddTransient<Base, Baz>()
            //    .BuildServiceProvider()
            //    .GetServices<Base>();
            //Debug.Assert(service.OfType<Foo>().Any());
            //Debug.Assert(service.OfType<Bar>().Any());
            //Debug.Assert(service.OfType<Baz>().Any());

            //1.生命周期
            /*
             * 
             *  Singleton：由于 Singleton 服务实例保存在作为根容器的IServiceProvider对象上，所以它能够在多个同根，IServiceProvider对象之间提供真正的单例保证
                Scoped：Scoped服务实例被保存在当前 IServiceProvider对象上，所以它只能在当前范围内保证提供的实例是单例的
                Transient：没有实现 IDisposable 接口的Transient服务则采用“即用即建，用后即弃”的策略
             * 
             */
            var root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar>(_ => new Bar())
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider();

            //创建第一个"服务范围"
            var provider1 = root.CreateScope().ServiceProvider;
            //创建第二个"服务范围"
            var provider2 = root.CreateScope().ServiceProvider;

            GetService<IFoo>(provider1);



            static void GetService<T>(IServiceProvider provider) 
            {
                provider.GetServices<T>();
                provider.GetServices<T>();
            }
        }
    }
}
