using DI_04.Interface;
using DI_04.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;

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
            //var root = new ServiceCollection()
            //    .AddTransient<IFoo, Foo>()
            //    .AddScoped<IBar>(_ => new Bar())
            //    .AddSingleton<IBaz, Baz>()
            //    .BuildServiceProvider();

            ////创建第一个"服务范围"
            //var provider1 = root.CreateScope().ServiceProvider;
            ////创建第二个"服务范围"
            //var provider2 = root.CreateScope().ServiceProvider;

            //GetService<IFoo>(provider1);
            //GetService<IBar>(provider1);
            //GetService<IBaz>(provider1);
            //Console.WriteLine();
            //GetService<IFoo>(provider2);
            //GetService<IBar>(provider2);
            //GetService<IBaz>(provider2);


            //static void GetService<T>(IServiceProvider provider) 
            //{
            //    provider.GetServices<T>();
            //    provider.GetServices<T>();
            //}

            //2.生命周期
            //using (var root = new ServiceCollection()
            //    .AddTransient<IFoo, Foo>()
            //    .AddScoped<IBar, Bar>()
            //    .AddSingleton<IBaz, Baz>()
            //    .BuildServiceProvider())
            //{
            //    using (var scope = root.CreateScope()) 
            //    {
            //        var provider = scope.ServiceProvider;
            //        provider.GetService<IFoo>();
            //        provider.GetService<IBar>();
            //        provider.GetService<IBaz>();
            //        Console.WriteLine("Child container is disposed.");
            //    }
            //    Console.WriteLine("Root container is disposed.");
            //}
            //Console.ReadLine();

            //3.针对服务注册的验证 第一
            //Console.WriteLine("开始...");
            //var root = new ServiceCollection()
            //    .AddTransient<IFoo, Foo>()
            //    .AddScoped<IBar, Bar>()
            //    .BuildServiceProvider(true);
            //var child = root.CreateScope().ServiceProvider;

            //void ResolveService<T>(IServiceProvider provider) 
            //{
            //    var isRootContainer = root == provider ? "Yes" : "No";
            //    try
            //    {
            //        provider.GetService<T>();
            //        Console.WriteLine($"Status: Success; Service Type: {typeof(T).Name}; Root: {isRootContainer}");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Status: Fall; Service Type:{typeof(T).Name}; Root: {isRootContainer}");
            //        Console.WriteLine($"Error:{ex.Message}");
            //    }
            //}

            //ResolveService<IFoo>(root);
            //ResolveService<IBar>(root);
            //ResolveService<IFoo>(child);
            //ResolveService<IBar>(child);

            //3.针对服务注册的验证
            /*
             * 由于Foobar具有唯一的私有构造函数，而内嵌方法BuildServiceProvider提供的服务注册并不能提供我们所需的服务实例，所以这个服务注册是无效的
             * 由于在默认情况下构建IServiceProvider 对象的时候并不会对服务注册做有效性检验，所以此时无效的服务注册并不会及时被探测到。一旦将 ValidateOnBuild选项设置为 True，IServiceProvider对象在被构建的时候就会抛出异常
             */
            BulidServiceProvider(false);
            BulidServiceProvider(true);
            static void BulidServiceProvider(bool vaildateOnBuild) 
            {
                try
                {
                    var options = new ServiceProviderOptions 
                    {
                        ValidateOnBuild = vaildateOnBuild
                    };
                    new ServiceCollection()
                        .AddSingleton<IFoobar, Foobar>()
                        .BuildServiceProvider(options);

                    Console.WriteLine($"Status: Success; ValidateOnBuild: {vaildateOnBuild}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Status: Fail; ValidateOnBuild: {vaildateOnBuild}");
                    Console.WriteLine($"Error:{ex.Message}");
                }
            }

            Console.ReadLine();
        }
    }
}
