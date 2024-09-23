using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Configuration;

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
            /*
                    我们依然沿用前面演示的应用场景，现在摒弃配置文件，转而采用编程的方式直接对用户信息进行初始化。
                所以需要对程序做如下改写。
             */
            var optionsAccessor = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>("foo", it =>
                {
                    it.Gender = Gender.Male;
                    it.Age = 18;
                    it.ContactInfo = new ContactInfo
                    {
                        PhoneNo = "123456789",
                        EmailAddress = "foo@outlook.com"
                    };
                }).Configure<Profile>("bar", it => 
                {
                    it.Gender = Gender.Female;
                    it.Age = 25;
                    it.ContactInfo = new ContactInfo
                    {
                        PhoneNo = "456",
                        EmailAddress = "bar@outlook.com"
                    };
                })
                .BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<Profile>>();
            Print(optionsAccessor.Get("foo"));
            Print(optionsAccessor.Get("bar"));
            #endregion
            #endregion
            #endregion

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
