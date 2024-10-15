using System;
using System.Configuration;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;

namespace Configuration7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 7.配置选项（）
            /*
                  .NET Core组件、框架和应用基本上都会将配置选项绑定为一个
                POCO对象，并以依赖注入的形式来使用它。我们将这个承载配置选
                项的 POCO对象称为 Options对象，将这种以依赖注入方式来消费它
                的编程方式称为 Options 模式（Options Pattern）。第 6 章介绍的配
                置系统是Options对象的主要数据来源，除了将承载配置数据的
                IConfiguration 对象绑定为 Options 对象，我们还可以直接通过编程
                的方式来初始化Options对象
             */
            #region 7.1 Options 模式
            /*
                    依赖注入不仅是支撑整个ASP.NETCore框架的基石，也是开发
                ASP.NETCore应用采用的基本编程模式，所以依赖注入十分重要。
                依赖注入使我们可以将依赖的功能定义成服务，最终以一种松耦合的
                形式注入消费该功能的组件或者服务中。除了可以采用依赖注入的形
                式消费承载某种功能的服务，还可以采用相同的方式消费承载配置数
                据的Options对象
             */
            #region 7.1.1 将配置绑定为Options对象
            ///*
            //        下面通过一个简单的控制台应用来演示Options编程模式。在演
            //    示程序中定义了上面这些类型之后，我们创建承载一个 Profile 对象
            //    的配置文件 profile.json

            //    采用Options模式由配置文件提供的数据绑定生成的Profile对象
            // */
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("profile.json")
            //    .Build();
            //var profile = new ServiceCollection()
            //    .AddOptions()
            //    .Configure<Profile>(configuration)
            //    .BuildServiceProvider()
            //    .GetRequiredService<IOptions<Profile>>()
            //    .Value;

            //Console.WriteLine($"Gender:{profile.Gender}");
            //Console.WriteLine($"Age:{profile.Age}");
            //Console.WriteLine($"Email Address:{profile.ContactInfo.EmailAddress}");
            //Console.WriteLine($"Phone No:{profile.ContactInfo.PhoneNo}");
            #endregion

            #region 7.1.2 提供具名的Options
            ///*
            // 提供原始配置数据的IConfiguration对象也由原来的ConfigurationRoot对象变成它的两个子配置节
            // */
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("profile.json")
            //    .Build();

            //var serviceProvider = new ServiceCollection()
            //    .AddOptions()
            //    .Configure<Profile>("foo", configuration.GetSection("foo"))
            //    .Configure<Profile>("bar", configuration.GetSection("bar"))
            //    .BuildServiceProvider();

            //var optionsAccessor = serviceProvider
            //    .GetRequiredService<IOptionsSnapshot<Profile>>();

            //Print(optionsAccessor.Get("foo"));
            //Print(optionsAccessor.Get("bar"));
            #endregion

            #region 7.1.3 配置源的同步
            ///*
            //        下面对它做相应的修改来演示如何监控这个JSON 文件，并在监测到文件改变之后及时提取新的配置信息生成新的 Profile 对象。
            //    如下面的代码片段所示，调用 AddJsonFile 扩展方法注册对应配置源时应将该方法的参数reloadOnChange设置为True，从而开启
            //    对对应配置文件的监控功能。
            // */
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile(path: "profile.json", optional: false, reloadOnChange: true)
            //    .Build();

            //new ServiceCollection()
            //   .AddOptions()
            //   .Configure<Profile>("bar", configuration.GetSection("bar"))
            //   .Configure<Profile>("bar", configuration.GetSection("bar"))
            //   .BuildServiceProvider()
            //   .GetRequiredService<IOptionsMonitor<Profile>>()
            //   .OnChange((profile, name) =>
            //   {
            //       Console.WriteLine($"Name:{name}");
            //       Print(profile);
            //   });
            //Console.Read();
            #endregion

            #region 7.1.4 直接初始化Options对象
            ///*
            //        我们依然沿用前面演示的应用场景，现在摒弃配置文件，转而采用编程的方式直接对用户信息进行初始化。
            //    所以需要对程序做如下改写。
            // */
            //var optionsAccessor = new ServiceCollection()
            //    .AddOptions()
            //    .Configure<Profile>("foo", it =>
            //    {
            //        it.Gender = Gender.Male;
            //        it.Age = 18;
            //        it.ContactInfo = new ContactInfo
            //        {
            //            PhoneNo = "123456789",
            //            EmailAddress = "foo@outlook.com"
            //        };
            //    }).Configure<Profile>("bar", it => 
            //    {
            //        it.Gender = Gender.Female;
            //        it.Age = 25;
            //        it.ContactInfo = new ContactInfo
            //        {
            //            PhoneNo = "456",
            //            EmailAddress = "bar@outlook.com"
            //        };
            //    })
            //    .BuildServiceProvider()
            //    .GetRequiredService<IOptionsMonitor<Profile>>();
            //Print(optionsAccessor.Get("foo"));
            //Print(optionsAccessor.Get("bar"));
            #endregion

            #region 7.1.5 根据依赖服务的Options设置
            //var environment = new ConfigurationBuilder()
            //        .AddCommandLine(args)
            //        .Build()["env"];

            //var services = new ServiceCollection();
            //services.AddSingleton<IHostEnvironment>(new HostingEnvironment { EnvironmentName = environment })
            //    .AddOptions<DateTimeFormatOptions>().Configure<IHostEnvironment>((options, env) => 
            //    {
            //        if (env.IsDevelopment())
            //        {
            //            options.DatePattern = "dddd, MMMM d, yyyy";
            //            options.TimePattern = "M/d/yyyy";
            //        }
            //        else 
            //        {
            //            options.DatePattern = "M/d/yyyy";
            //            options.TimePattern = "h:mm tt";
            //        }
            //    });

            //var options = services.BuildServiceProvider()
            //                       .GetRequiredService<IOptions<DateTimeFormatOptions>>().Value;
            //Console.WriteLine(options);


            #endregion

            #region 7.1.6 验证Options的有效性
            ///*
            //        由于配置选项是整个应用的全局设置，为了尽可能避免错误的设
            //    置造成的影响，最好能够对内容进行有效性验证。接下来我们将上面
            //    的程序做了如下改动，从而演示如何对设置的日期和时间格式做最后
            //    的有效性验证
            // */
            //var config = new ConfigurationBuilder()
            //    .AddCommandLine(args)
            //    .Build();

            //var datePattern = config["date"];
            //var timePattern = config["time"];

            //var services = new ServiceCollection();
            //services.AddOptions<DateTimeFormatOptions>()
            //    .Configure(options =>
            //    {
            //        options.DatePattern = datePattern;
            //        options.TimePattern = timePattern;
            //    }).Validate(options => Validate(options.DatePattern) && Validate(options.TimePattern), "Invalid Date or Time pattern.");

            //try
            //{
            //    var options = services
            //        .BuildServiceProvider()
            //        .GetRequiredService<IOptions<DateTimeFormatOptions>>().Value;
            //    Console.WriteLine(options);
            //}
            //catch (OptionsValidationException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            #endregion

            #endregion

            #region 7.2 Options模型
            /*    
                    通过前面演示的几个实例，我们已经对基于Options的编程方式
                有了一程度的了解，下面从设计的角度介绍 Options模型。我们演
                示的例已经涉及
                Optons模型的3个重要的接口，它们分别是
                IOpions＜TOptions＞和IOptionsSnapshot＜TOptions＞，最终的
                Optios对象正是利用它们来提供的。在 Options 模型中，这两个接
                口具有同一个实现类型 OptionsManager＜TOptions＞。Options模型
                的核心接口和类型定义在NuGet包“Microsoft.Extensions.Options”中
             */

            #region 7.2.1 OptionsManager<TOptions>
            //OptionsManager
            #endregion

            #region 7.2.2 IOptionsFactory
            /*
                    OptionsFactory＜TOptions＞是
                IOptionsFactory＜TOptions＞
                接口的默认实现。OptionsFactory＜TOptions＞对象针对 Options 对
                象的创建主要分 3 个步骤来完成，笔者将这 3 个步骤称为Options对
                象相关的“实例化”、“初始化”和“验证”。
             */
            #endregion

            #region 7.2.3 IOptionsMonitorCache＜TOptions＞
            /*
                    IOptionsFactory＜TOptions＞解决了 Options 的创建与初始化问
                题，但由于它自身是无状态的，所以Options模型对Options对象实施
                缓存可以获得更好的性能。Options模型中针对Options对象的缓存由
                IOptionsMonitorCache＜TOptions＞对象来完成，如下所示的代码片
                段是该接口的定义
             */
            #endregion

            #region 7.2.4 IOptionsMonitor＜TOptions＞
            /*
                    Options
                模型之所以将表示缓存的接口命名为
                IOptionsMonitorCache＜TOptions＞，是因为缓存最初是为
                IOptionsMonitor＜TOptions＞对象服务的，该对象旨在实现针对承
                载Options对象的原始数据源的监控，并在检测到数据更新后及时替
                换缓存的Options对象
             */
            #endregion

            #region 7.3 依赖注入
            #region 7.3.1 服务注册
            ////AddOptions 服务注册
            ///*
            //        AddOptions 扩展方法的完整定义如下所示，由此可知，该方法
            //    Options
            //    模型中的几个核心类型作为服务注册到了指定的
            //    IServiceCollection对象之中。由于它们都是调用TryAdd方法进行服
            //    务注册的，所以我们可以在需要
            //    Options
            //    模式支持的情况下调用
            //    AddOptions 方法，而不需要担心是否会添加太多重复服务注册的问
            //    题
            // */
            //     public static class OptionsServiceCollectionExtensions
            //    {
            //        /// <summary>
            //        /// Adds services required for using options.
            //        /// </summary>
            //        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
            //        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
            //        public static IServiceCollection AddOptions(this IServiceCollection services)
            //        {
            //            ThrowHelper.ThrowIfNull(services);

            //            services.TryAdd(ServiceDescriptor.Singleton(typeof(IOptions<>), typeof(UnnamedOptionsManager<>)));
            //            services.TryAdd(ServiceDescriptor.Scoped(typeof(IOptionsSnapshot<>), typeof(OptionsManager<>)));
            //            services.TryAdd(ServiceDescriptor.Singleton(typeof(IOptionsMonitor<>), typeof(OptionsMonitor<>)));
            //            services.TryAdd(ServiceDescriptor.Transient(typeof(IOptionsFactory<>), typeof(OptionsFactory<>)));
            //            services.TryAdd(ServiceDescriptor.Singleton(typeof(IOptionsMonitorCache<>), typeof(OptionsCache<>)));
            //            return services;
            //        } 
            //    }
            // /*
            //        从上面代码可以看出，AddOptions扩展方法实际上注册了5个服务
             
             
            // */
            #endregion
            #endregion
            #endregion
            #endregion

        }

        static bool Validate(string format)
        {
            var time = new DateTime(1981, 8, 24, 2, 2, 2);
            var formatted = time.ToString();
            return DateTimeOffset.TryParseExact(formatted, format, null, DateTimeStyles.None, out var value) && (value.Date == time.Date || value.TimeOfDay == time.TimeOfDay);
        }

        static void Print(Profile profile) 
        {
            Console.WriteLine($"Gender:{profile.Gender}");
            Console.WriteLine($"Age:{profile.Age}");
            Console.WriteLine($"Email Address:{profile.ContactInfo.EmailAddress}");
            Console.WriteLine($"Phone No:{profile.ContactInfo.PhoneNo}\n");
        }
    }
}
