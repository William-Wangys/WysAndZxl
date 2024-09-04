using DI_04.Interface;
using DI_04.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.Extensions.Primitives;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DI_04
{
    internal class Program
    {
        static async Task Main(string[] args)
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

            ////创建第一个"服务范围"吗
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
            //        Console.WriteLine($"  :{ex.Message}");
            //    }
            //}

            //ResolveService<IFoo>(root);
            //ResolveService<IBar>(root);
            //ResolveService<IFoo>(child);
            //ResolveService<IBar>(child);


            #region 3.针对服务注册的验证
            /*
            * 由于Foobar具有唯一的私有构造函数，而内嵌方法BuildServiceProvider提供的服务注册并不能提供我们所需的服务实例，所以这个服务注册是无效的
            * 由于在默认情况下构建IServiceProvider 对象的时候并不会对服务注册做有效性检验，所以此时无效的服务注册并不会及时被探测到。一旦将 ValidateOnBuild选项设置为 True，IServiceProvider对象在被构建的时候就会抛出异常
            */
            //BulidServiceProvider(false);
            //BulidServiceProvider(true);
            //static void BulidServiceProvider(bool vaildateOnBuild) 
            //{
            //    try
            //    {
            //        var options = new ServiceProviderOptions 
            //        {
            //            ValidateOnBuild = vaildateOnBuild
            //        };
            //        var bulid = new ServiceCollection()
            //            .AddSingleton<IFoobar, Foobar>()
            //            .BuildServiceProvider(options);

            //        Console.WriteLine($"Status: Success; ValidateOnBuild: {   }");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Status: Fail; ValidateOnBuild: {vaildateOnBuild}");
            //        Console.WriteLine($"Error:{ex.Message}");
            //    }
            //}

            //Console.ReadLine();
            #endregion

            #region 4.3 服务和消费


            ////4.3 服务和消费
            ///*
            // * 包含服务注册信息的 IServiceCollection 集合最终被用来创建作
            //    为依赖注入容器的IServiceProvider 对象。当需要消费某个服务实例
            //    的时候，只需要指定服务类型调用IServiceProvider
            //    接口的
            //    GetService 方法即可，IServiceProvider
            // */

            ////4.3.1  IServiceProvider

            ////4.3.2  服务实例的创建

            ////在所有合法的候选构造函数列表中，最终被选择的构造函数具有如下特征：每个候选构造函数的参数类型集合都是这个构造函数参数类型集合的子集
            //Console.WriteLine();
            //new ServiceCollection()
            //    .AddTransient<IFoo, Foo>()
            //    .AddTransient<IBar, Bar>()
            //    .AddTransient<IBaz, Baz>()
            //    .AddTransient<IQux, Qux>()
            //    .BuildServiceProvider()
            //    .GetService<IQux>();
            //Console.ReadLine();
            #endregion

            #region 4.3.3 生命周期
            //服务范围
            //1.Singleton：提供Disposable服务实例保存在作为根容器的IServiceProvider对象上，只有在这个IServiceProvider对象被释放的时候，这些Disposable服务实例才能被释放
            //2.Scoped 和Transient：IServiceProvider对象会保存由它提供的 Disposable服务实例,当自己被释放的时候，这些Disposable服务实例就会被释放
            #endregion

            #region ServiceProvider
            /*
             * ServiceProviderEngine的唯一性：整个服务提供体系只存在一个 ServiceProviderEngine对象
             * ServiceProviderEngine与IServiceFactory的同一性：：唯一存在的ServiceProviderEngine会作为创建服务范围的IServiceFactory工厂。
             * ServiceProviderEngineScope和IServiceProvider的同一性：表示服务范围的ServiceProviderEngineScope也作为服务提供者的依赖注入容器。
             */
            //var (engineType, engineScopeType) = ResolveTypes();
            //var root = new ServiceCollection().BuildServiceProvider();
            //var child1 = root.CreateScope().ServiceProvider;
            //var child2 = root.CreateScope().ServiceProvider;

            //var engine = GetEngine(root);
            //var rootScope = GetRootScope(engine, engineType);

            ////ServiceProviderEngine的唯一性
            //Debug.Assert(ReferenceEquals(GetEngine(rootScope, engineScopeType), engine));
            //Debug.Assert(ReferenceEquals(GetEngine(child1, engineScopeType), engine));
            //Debug.Assert(ReferenceEquals(GetEngine(child2, engineScopeType), engine));

            ////ServiceProviderEngine 和 IServiceScopeFactory 的同一性
            //Debug.Assert(ReferenceEquals(root.GetRequiredService<IServiceScopeFactory>(), engine));
            //Debug.Assert(ReferenceEquals(child1.GetRequiredService<IServiceScopeFactory>(), engine));
            //Debug.Assert(ReferenceEquals(child2.GetRequiredService<IServiceScopeFactory>(), engine));

            ////ServiceProviderEngineScope提供的IServiceProvider 是它自己
            ////ServiceProvider提供的IServiceProvider是RootScope
            //Debug.Assert(ReferenceEquals(root.GetRequiredService<IServiceProvider>(), rootScope));
            //Debug.Assert(ReferenceEquals(child1.GetRequiredService<IServiceProvider>(), child1));
            //Debug.Assert(ReferenceEquals(child2.GetRequiredService<IServiceProvider>(), child2));

            ////ServiceProviderEngineScope和IServiceProvider的同一性
            //Debug.Assert(ReferenceEquals((rootScope).ServiceProvider, rootScope));
            //Debug.Assert(ReferenceEquals(((IServiceScope)child1).ServiceProvider, child1));
            //Debug.Assert(ReferenceEquals(((IServiceScope)child2).ServiceProvider, child2));
            #endregion

            #region 5 文件系统

            #region 5.1抽象文件系统
            #region 5.1.1 树形层次结构
            //static void Print(int layer, string name) => Console.WriteLine($"{new string(' ', layer * 4)}{name}");

            //new ServiceCollection()
            //    .AddSingleton<IFileManager, FileManager>()
            //    .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"d:\test"))
            //    .BuildServiceProvider()
            //    .GetRequiredService<IFileManager>()
            //    .ShowStructure(Print);
            #endregion

            #region 5.1.2 读取文件内容
            //var assembly = Assembly.GetExecutingAssembly();

            //var content1 = await new ServiceCollection()
            //    .AddSingleton<IFileProvider>(new EmbeddedFileProvider(assembly))
            //    .AddSingleton<IFileManager, FileManager>()
            //    .BuildServiceProvider()
            //    .GetRequiredService<IFileManager>()
            //    .ReadAllTestAsync("data.txt");

            //var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.data.txt");
            //var buffer = new byte[stream.Length];
            //stream.Read(buffer, 0, buffer.Length);
            //var content2 = Encoding.Default.GetString(buffer);

            //Debug.Assert(content2 == content1);
            //Console.Read();
            #endregion

            #region 5.1.3 监听文件的变化
            using (var fileProvider = new PhysicalFileProvider(@"d:\test"))
            {
                string original = null;
                ChangeToken.OnChange(() => fileProvider.Watch("data.txt"), Callback);

                while (true)
                {
                    File.WriteAllText(@"d:\test\data.txt", DateTime.Now.ToString());
                    await Task.Delay(5000);
                }

                async void Callback()
                {
                    var stream = fileProvider.GetFileInfo("data.txt").CreateReadStream();
                    {
                        var buffer = new byte[stream.Length];
                        await stream.ReadAsync(buffer, 0, buffer.Length);
                        string current = Encoding.Default.GetString(buffer);
                        if (current != original)
                        {
                            Console.WriteLine(current);
                            Console.WriteLine(current = original);
                        }
                    }
                }
            }
            #endregion
            #endregion
            #region 5.2 设计讲解
            #region 5.2.1 IChangeToken
            //4


            #endregion

            #region 5.2.2 IFileProvider
            //路径不包含前缀"/"
            //var dirContents =
            #endregion
            #endregion

            #endregion

           


        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <returns></returns>
        static (Type Engine, Type EngineScope) ResolveTypes()
        {
            var assembly = typeof(ServiceProvider).Assembly;
            var engine = assembly.GetTypes().Single(it => it.Name == "IServiceProviderEngine");
            var engineScope = assembly.GetTypes().Single(it => it.Name == "ServiceProviderEngineScope");
            return (engine, engineScope);
        }

        /// <summary>
        /// 获取引擎
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        static object GetEngine(ServiceProvider serviceProvider)
        {
            var field = typeof(ServiceProvider).GetField("_engine", BindingFlags.Instance | BindingFlags.NonPublic);
            return field.GetValue(serviceProvider);
        }

        /// <summary>
        /// 获取引擎
        /// </summary>
        /// <param name="enginScope"></param>
        /// <param name="engineScopeType"></param>
        /// <returns></returns>
        static object GetEngine(object enginScope, Type engineScopeType)
        {
            var property = engineScopeType.GetProperty("Engine", BindingFlags.Instance | BindingFlags.Public);
            return property.GetValue(enginScope);
        }

        /// <summary>
        /// 获取跟作用域
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="engineType"></param>
        /// <returns></returns>
        static IServiceScope GetRootScope(object engine, Type engineType)
        {
            var property = engineType.GetProperty("RootScope", BindingFlags.Instance | BindingFlags.Public);
            return (IServiceScope)property.GetValue(engine);
        }
    }
}