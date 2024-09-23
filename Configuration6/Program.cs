using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Configuration6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 6.配置选项（上篇）
            #region 6.1 读取配置信息
            /*
             *新的配置系统具有更好的扩展性，其最大的特点就是支持多样化的数据源，也可以将配置定义在持久化的文件甚至数据库中。
             *在对配置系统进行系统介绍之前，下面先从编程的角度阐述全新的配置读取方式
             */
            #region 6.1.1 配置编程模型三要素
            /*
             * 就编程层面来讲,.NET Core的配置系统由3个核心对象构成
             * IConfigurationSource > IConfigurationBuilder > IConfiguration
             * 
             * IConfiguration：读取的配置信息最终会转换成一个 IConfiguration 对象供应用程序使用
             * IConfigurationBuilder：IConfigurationBuilder 对象是IConfiguration对象的构建者
             * IConfigurationSource：IConfigurationSource对象则代表配置数据最原始的来源
             * 
             * 在读取配置时，可以根据配置的定义方式（数据源）创建相应的
               IConfigurationSource 对象，并将其注册到 IConfigurationBuilder对
               象上。
             */
            #endregion
            #region 6.1.2 以键值对的形式读取配置
            ///*
            //   假设应用程序需要通过配置来设定日期/时间的显示格式，所以
            //   可以将相关的配置信息定义在如下所示的
            //   DateTimeFormatOptions类中，它的 4个属性体现了针对 DateTime对象的 4种显示格式（分别为长日期/时间和短日期/时间）
            // */
            //var source = new Dictionary<string, string>
            //{
            //    ["longDatePattern"] = "dddd, MMMM d, yyyy",
            //    ["longTimePattern"] = "h:mm:ss tt",
            //    ["shortDatePattern"] = "M/d/yyyy",
            //    ["shortTimePattern"] = "h:mm tt"
            //};

            //var config =new ConfigurationBuilder()
            //    .Add(new MemoryConfigurationSource {InitialData =source })
            //    .Build();

            //var options = new DateTimeFormatOptions(config);
            //Console.WriteLine($"LongDatePattern:{options.LongDatePattern}");
            //Console.WriteLine($"LongTimePattern:{options.LongTimePattern}");
            //Console.WriteLine($"ShortDatePattern:{options.ShortDatePattern}");
            //Console.WriteLine($"ShortTimePattern:{options.ShortTimePattern}");
            #endregion
            #region 6.1.3 读取结构化的配置
            ///*
            //  真实项目中涉及的配置大都具有结构化的层次结构，所以IConfiguration对象同样具有这样的结构

            //  现在不仅需要设置日期/时间的格式，还需要设置其他数据类型的格式，如表示货币的Decimal 类型
            //  因此，我们定义了一个 CurrencyDecimalFormatOptions 类，它的Digits属性和Symbol属性分别表示小数位数与货币符号，
            //  CurrencyDecimalFormatOptions 对象依然是利用IConfiguration对象创建的

            //下面的代码片段按照表6-1列举的结构创建了一个Dictionary＜string，string＞对象，并利用它创建了MemoryConfigurationSource对象。
            //在利用ConfigurationBuilder对象得到IConfiguration对象之后，我们调用其 GetSection 方法得到名称为 Format 的配置节，并利用后者创建了一个FormatOptions
            // */

            //var source = new Dictionary<string, string>()
            //{
            //    ["format:dateTime:longDatePattern"] = "dddd, MMMM d, yyyy",
            //    ["format:dateTime:longTimePattern"] = "h:mm:ss tt",
            //    ["format:dateTime:shortDatePattern"] = "M/d/yyyy",
            //    ["format:dateTime:shortTimePattern"] = "h:mm tt",

            //    ["format:currencyDecimal:digits"] = "2",
            //    ["format:currencyDecimal:symbol"] = "$"
            //};

            //var configuration = new ConfigurationBuilder()
            //    .Add(new MemoryConfigurationSource { InitialData = source })
            //    .Build();

            //var options = new FormatOptions(configuration.GetSection("Format"));
            //var dateTime = options.DateTime;
            //var currencyDecimal = options.CurrencyDecimal;

            //Console.WriteLine("DateTime:");
            //Console.WriteLine($"\tLongDatePattern:{dateTime.LongDatePattern}");
            //Console.WriteLine($"\tLongTimePattern:{dateTime.LongTimePattern}");
            //Console.WriteLine($"\tShortDatePattern:{dateTime.ShortDatePattern}");
            //Console.WriteLine($"\tShortTimePattern:{dateTime.ShortTimePattern}");

            //Console.WriteLine("CurrencyDecimal:");
            //Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
            //Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");
            #endregion
            #region 6.1.4 将结构化配置直接绑定为对象
            ///*
            //  如果承载配置数据的IConfiguration对象与对应的POCO类型具有
            //  兼容的结构，那么利用配置的自动绑定机制可以将IConfiguration对
            //  象直接转换成对应的POCO对象。对于我们演示的实例来说，如果采
            //  用自动化配置绑定来创建对应的Options对象，那么这些类型中就不
            //  再需要实现手动绑定的构造函数

            //  在删除所有 Options类型的构造函数之后，再修改 Options对象
            //  的创建方式。如下面的代码片段所示，在调用IConfigurationBuilder
            //  对象的Build方法创建出对应IConfiguration对象之后，调用
            //  GetSection方法可以得到其format配置节，而FormatOptions对象不
            //  用再通过调用构造函数来创建，而是直接调用该配置节的 Get＜T＞
            //  方法，该方法完成了从 IConfiguration对象到 POCO对象之间的自动化绑定。
            // */
            //var source = new Dictionary<string, string>()
            //{
            //    ["format:dateTime:longDatePattern"] = "dddd, MMMM d, yyyy",
            //    ["format:dateTime:longTimePattern"] = "h:mm:ss tt",
            //    ["format:dateTime:shortDatePattern"] = "M/d/yyyy",
            //    ["format:dateTime:shortTimePattern"] = "h:mm tt",

            //    ["format:currencyDecimal:digits"] = "2",
            //    ["format:currencyDecimal:symbol"] = "$"
            //};

            //var options = new ConfigurationBuilder()
            //    .Add(new MemoryConfigurationSource { InitialData = source })
            //    .Build()
            //    .GetSection("format")
            //    .Get<FormatOptions>();

            //var dateTime = options.DateTime;
            //var currencyDecimal = options.CurrencyDecimal;

            //Console.WriteLine("DateTime:");
            //Console.WriteLine($"\tLongDatePattern:{dateTime.LongDatePattern}");
            //Console.WriteLine($"\tLongTimePattern:{dateTime.LongTimePattern}");
            //Console.WriteLine($"\tShortDatePattern:{dateTime.ShortDatePattern}");
            //Console.WriteLine($"\tShortTimePattern:{dateTime.ShortTimePattern}");

            //Console.WriteLine("CurrencyDecimal:");
            //Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
            //Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");
            #endregion
            #region 6.1.5 将配置定义在文件中
            /*
                由于配置源发生了改变，原来的MemoryConfigurationSource
                需要替换成JsonConfigurationSource，但不需要手动创建这个
                JsonConfigurationSource 对象，只需要调用 IConfigurationBuilder
                接口的AddJsonFile扩展方法添加指定的JSON文件即可
             */
            #region 采用 JSON 文件作为配置源
            ////针对执行环境的判断以及针对环境的配置加载体现在如下所示的代码片段中
            //var index = Array.IndexOf(args, "/env");
            //var environment = index > -1 ? args[index + 1] : "Development";

            //var options = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .AddJsonFile($"appsettings.{environment}.json", true)
            //    .Build()
            //    .GetSection("format")
            //    .Get<FormatOptions>();

            //var dateTime = options.DateTime;
            //var currencyDecimal = options.CurrencyDecimal;

            //Console.WriteLine("DateTime:");
            //Console.WriteLine($"\tLongDatePattern:{dateTime.LongDatePattern}");
            //Console.WriteLine($"\tLongTimePattern:{dateTime.LongTimePattern}");
            //Console.WriteLine($"\tShortDatePattern:{dateTime.ShortDatePattern}");
            //Console.WriteLine($"\tShortTimePattern:{dateTime.ShortTimePattern}");

            //Console.WriteLine("CurrencyDecimal:");
            //Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
            //Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");
            #endregion

            #region 配置文件的同步
            ////前面演示的应用程序采用 JSON 文件作为配置源，所以我们希望应用程序能够感知该文件的改变
            ////并在文件发生改变的时候自动加载 新的配置，然后将其重新应用到程序之中。
            //var config = new ConfigurationBuilder()
            //    .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
            //    .Build();

            //ChangeToken.OnChange(() => config.GetReloadToken(), () => 
            //{
            //    var options = config.GetSection("format").Get<FormatOptions>();
            //    var dateTime = options.DateTime;
            //    var currencyDecimal = options.CurrencyDecimal;

            //    Console.WriteLine("DateTime:");
            //    Console.WriteLine($"\tLongDatePattern:{dateTime.LongDatePattern}");
            //    Console.WriteLine($"\tLongTimePattern:{dateTime.LongTimePattern}");
            //    Console.WriteLine($"\tShortDatePattern:{dateTime.ShortDatePattern}");
            //    Console.WriteLine($"\tShortTimePattern:{dateTime.ShortTimePattern}");

            //    Console.WriteLine("CurrencyDecimal:");
            //    Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
            //    Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");
            //});
            //Console.Read();
            #endregion
            #endregion
            #endregion

            #region 6.2 配置模型
            /*
             6.1 节通过实例演示了几种典型的配置读取方式，下面从设计的角度重写认识配置模型。
            配置的编程模型涉及 3 个核心对象，分别通过 3 个对应的接口（IConfiguration、IConfiguration Source 和IConfigurationBuilder）来表示
             */
            #region 6.2.1 数据结构及其转换
            /*
             配置模型的最终目的在于提取原始的配置数据并将其转换成一个 IConfiguration对象

             配置从原始结构向逻辑结构的转变不是一蹴而就的，在它们之间有一种中间结构
             */
            #endregion

            #region 6.2.2 IConfiguration
            ///*
            //    我们以不同的方式调用 GetSection方法
            //    得到的都是路径为“A：B：C”的 IConfigurationSection 对象。上面这
            //    段代码还体现了另一个有趣的现象：虽然这3个IConfigurationSection 对象均指向配置树的同一个节点，但是它们却
            //    并非同一个对象。换句话说，调用 GetSection 方法时，不论配置树
            //    中是否存在一个与指定路径相匹配的配置节，它总是会创建新的IConfigurationSection对象
            // */
            //var source = new Dictionary<string, string>()
            //{
            //    ["A:B:C"] = "ABC"
            //};

            //var root = new ConfigurationBuilder()
            //    .AddInMemoryCollection(source)
            //    .Build();

            //var section1 = root.GetSection("A:B:C");
            //var section2 = root.GetSection("A:B").GetSection("C");
            //var section3 = root.GetSection("A").GetSection("B:C");

            //Debug.Assert(section1.Value == "ABC");
            //Debug.Assert(section2.Value == "ABC");
            //Debug.Assert(section3.Value == "ABC");

            //Debug.Assert(ReferenceEquals(section1, section2));
            //Debug.Assert(ReferenceEquals(section1, section3));
            //Debug.Assert(null != root.GetSection("D"));
            //Console.Read();
            #endregion

            #region 6.2.3 IConfigurationProvider
            /*
                在 6.1 节介绍 IConfigurationSource 对象的时候，我们说它是对
                原始配置源的体现。虽然每种不同类型的配置源都有一个对应的
                IConfigurationSource 实现，但是针对原始数据的读取并不是由它完
                成的，而是委托一个与之对应的 IConfigurationProvider 对象来实
                现。在前面介绍的配置结构转换过程中，针对不同配置源类型的
                IConfigurationProvider对象可以按照图 6-11所示的方式实现配置数
                据从原始结构向物理结构的转换。

                由于 IConfigurationProvider 对象的目的在于将配置数据从原始
                结构转换成配置字典，所以定义在IConfigurationProvider接口中的
                方法大都体现为针对字典对象的操作。

                

                每种类型的配置源都对应一个 IConfigurationProvider 接口的实
                现类型，但它们一般不会直接实现 IConfigurationProvider接口，而
                是选择继承另一个名为 ConfigurationProvider的抽象类。这个抽象
                类的定义其实很简单，从如下代码片段可以看出，ConfigurationProvider
                仅仅是对一个IDictionary＜string，string＞对象（Key不区分大小写）的封装，
                其 Set方法和 TryGetValue方法最终操作的都是这个字典对象。
             
             */

            //IConfigurationProvider

            //ConfigurationProvider：
            //抽象类ConfigurationProvider实现了 Load方法并将其定义成虚方法，这个方法并没有提供具体的实现，所以它的派生类可以通过重写这个方法从相应的数据源中读取配置数据，并通过对Data属性的赋值完成对配置数据的加载。
            #endregion/

            #region 6.2.4 IConfigurationSource
            /*
                   IConfigurationSource对象在配置模型中代表配置源，被注册到
                IConfigurationBuilder对象上，为由它创建的 IConfiguration对象提
                供原始的配置数据。由于针对原始配置数据的读取在相应的
                IConfigurationProvider 对象中实现，所以 IConfigurationSource 对
                象的作用就在于提供相应的IConfigurationProvider对象。如下面的
                代码片段所示，IConfigurationSource接口具有一个唯一的Build方
                法，根据指定的IConfigurationBuilder对象提供对应的
                IConfigurationProvider对象。

                public interface IConfigurationSource
                {
                    IConfigurationProvider Build(IConfigurationBuilder builder);
                }
             */
            #endregion

            #region 6.2.5 IConfigurationBuilder
            /*
                IConfigurationBuilder 对象在整个配置模型中处于核心地位，代
             表原始配置源的IConfigurationSource对象就注册在它上面。
             

            总结：
            1、IConfigurationBuilder对象利用注册在它上面的所有IConfigurationBuilder对象利用注册在它上面的所有IConfigurationProvider对象来
            读取原始配置数据并创建出相应的IConfiguration对象。
             */
            #endregion
            #endregion

            #region 6.3 配置绑定
            /*
                虽然应用程序可以直接通过IConfigurationBuilder对象创建的IConfiguration对象来提取配置数据，但是我们更倾向于将其转换成
            一个POCO对象，以面向对象的方式来使用配置，我们将这个转换过程称为配置绑定。配置绑定可以通过如下几个针对IConfiguration的扩展
            方法来实现，这些扩展方法都定义在NuGet包"Microsoft.Extensions.Configuration.Binder"中
             
             */
            #region 6.3.1 绑定配置项的值
            ///*
            //  1、如果目标类型为object，那么直接返回原始值（字符串或者null）。
            //  2、如果目标类型不是Nullable<T>，那么针对目标类型的TypeConverter将用来做类型转换。
            //  3、如果目标类型是Nullable<T>，那么在原始值不是Null或者空字符串的情况下会将基础类型T作为新的目标类型进行转换，否则直接返回null
            // */
            //var source = new Dictionary<string, string>()
            //{
            //    ["foo"] = null,
            //    ["bar"] = "",
            //    ["baz"] = "123"
            //};

            //var root = new ConfigurationBuilder()
            //    .AddInMemoryCollection(source)
            //    .Build();

            ////针对object
            //Debug.Assert(root.GetValue<object>("foo") == null);
            //Debug.Assert("".Equals(root.GetValue<object>("bar")));
            //Debug.Assert("123".Equals(root.GetValue<object>("baz")));

            ////针对普通类型
            //Debug.Assert(root.GetValue<int>("foo") == 0);
            //Debug.Assert(root.GetValue<int>("baz") == 123);

            ////针对Nullable<T>
            //Debug.Assert(root.GetValue<int?>("foo") == null);
            //Debug.Assert(root.GetValue<int?>("bar") == null);

            ///*
            //    由于定义的Point类型支持源自字符串的类型转换，所以如果配置项的原始值（字符串）具有与之兼容的格式
            // 就可以按照如下方式将其绑定为一个Point对象。
            // */
            //var source = new Dictionary<string, string>()
            //{
            //    ["point"] = "(123,456)"
            //};

            //var root = new ConfigurationBuilder()
            //    .AddInMemoryCollection(source)
            //    .Build();

            //var point = root.GetValue<Point>("point");
            //Debug.Assert(point.X == 123);
            //Debug.Assert(point.Y == 456);
            #endregion

            #region 6.3.2 绑定复合数据类型
            ///*
            //    这里所谓的复合类型就是一个具有属性数据成员的自定义类型。

            //    可以通过下面的程序来验证针对复合数据类型的配置绑定。
            // */
            //var source = new Dictionary<string, string>()
            //{
            //    ["gender"] = "Male",
            //    ["age"] = "18",
            //    ["contactInfo:emailAddress"] = "foobar@outlook.com",
            //    ["contactInfo:phoneNo"] = "123456789"
            //};

            //var configuration = new ConfigurationBuilder()
            //    .AddInMemoryCollection(source)
            //    .Build();

            //var profile = configuration.Get<Profile>();

            //var obj1 = new ContactInfo { EmailAddress = "foobar@outlook.com", PhoneNo = "123456789" };
            //var obj2 = new ContactInfo { EmailAddress = "foobar@outlook.com", PhoneNo = "123456789" };
            //Debug.Assert(obj1 == obj2);

            //Debug.Assert(profile.Equals(new Profile(Gender.Male, 18, "foobar@outlook.com", "123456789")));
            #endregion

            #region 6.3.3 绑定集合对象
            ///*
            //    如果配置绑定的目标类型是一个集合（包括数组），那么当前 IConfiguration 对象的每个子配置节将绑定为集合的元素

            //    下面通过一个简单的实例来演示针对集合的配置绑定。如下面的
            //    代码片段所示，我们创建了一个 ConfigurationBuilder对象，并为它
            //    注册了一个 MemoryConfigurationSource对象
            // */

            //var source = new Dictionary<string, string>()
            //{
            //    ["foo:gender"] = "Male",
            //    ["foo:age"] = "18",
            //    ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
            //    ["foo:contactInfo:phoneNo"] = "123",

            //    ["bar:gender"] = "Male",
            //    ["bar:age"] = "25",
            //    ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
            //    ["bar:contactInfo:phoneNo"] = "457",

            //    ["baz:gender"] = "Female",
            //    ["baz:age"] = "36",
            //    ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
            //    ["baz:contactInfo:phoneNo"] = "789"
            //};

            //var configuration = new ConfigurationBuilder()
            //    .AddInMemoryCollection(source)
            //    .Build();

            //var profiles = new Profile[]
            //{
            //    new Profile(Gender.Male,18,"foo@outlook.com","123"),
            //    new Profile(Gender.Male,25,"bar@outlook.com","456"),
            //    new Profile(Gender.Female,36,"baz@outlook.com","789")
            //};

            //var collection = configuration.Get<IEnumerable<Profile>>();
            //Debug.Assert(collection.Any(it => it.Equals(profiles[0])));
            //Debug.Assert(collection.Any(it => it.Equals(profiles[1])));
            //Debug.Assert(collection.Any(it => it.Equals(profiles[2])));

            //var array = configuration.Get<Profile[]>();
            //Debug.Assert(array[0].Equals(profiles[0]));
            //Debug.Assert(array[1].Equals(profiles[1]));
            //Debug.Assert(array[2].Equals(profiles[2]));

            //var source = new Dictionary<string, string>()
            //{
            //    ["foo:gender"] = "Male",
            //    ["foo:age"] = "18",
            //    ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
            //    ["foo:contactInfo:phoneNo"] = "123",

            //    ["bar:gender"] = "Male",
            //    ["bar:age"] = "25",
            //    ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
            //    ["bar:contactInfo:phoneNo"] = "457",

            //    ["baz:gender"] = "Female",
            //    ["baz:age"] = "36",
            //    ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
            //    ["baz:contactInfo:phoneNo"] = "789"
            //};

            //var configuration = new ConfigurationBuilder()
            //    .AddInMemoryCollection(source)
            //    .Build();

            //var collection = configuration.Get<IEnumerable<Profile>>();
            //Debug.Assert(collection.Count() == 2);

            //var array = configuration.Get<Profile[]>();
            //Debug.Assert(array.Length == 3);
            //Debug.Assert(array[2] == null);
            ////由于配置节按照Key进行排序，所以绑定失败的配置节为最后一个
            #endregion

            #region 6.3.4 绑定字典
            ///*
            //    能够通过配置绑定生成的字典是一个实现了IDictionary＜
            //string，T＞的类型，也就是说，配置模型对字典的 Value类型没有任
            //何要求，但字典对象的 Key必须是一个字符串（或者枚举）。如果采
            //用配置树的形式表示这样一个字典对象，就会发现它与针对集合的配
            //置树在结构上几乎是一样的，唯一的区别是集合元素的索引直接变成
            //字典元素的Key
            // */
            //var source = new Dictionary<string, string>()
            //{
            //    ["foo:gender"] = "Male",
            //    ["foo:age"] = "18",
            //    ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
            //    ["foo:contactInfo:phoneNo"] = "123",

            //    ["bar:gender"] = "Male",
            //    ["bar:age"] = "25",
            //    ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
            //    ["bar:contactInfo:phoneNo"] = "457",

            //    ["baz:gender"] = "Female",
            //    ["baz:age"] = "36",
            //    ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
            //    ["baz:contactInfo:phoneNo"] = "789"
            //};

            //var configuration = new ConfigurationBuilder()
            //    .AddInMemoryCollection(source)
            //    .Build();

            //var profiles = configuration.Get<IDictionary<string, Profile>>();
            //Debug.Assert(profiles["foo"].Equals(new Profile(Gender.Male, 18, "foo@outlook.com", "123")));
            //Debug.Assert(profiles["bar"].Equals(new Profile(Gender.Male, 25, "bar@outlook.com", "457")));
            //Debug.Assert(profiles["baz"].Equals(new Profile(Gender.Female, 36, "baz@outlook.com", "789")));
            #endregion

            #endregion

            #region 6.4 配置的同步
            /*
                前面介绍配置模型的核心对象时，我们刻意回避了与配置同步相
            关的 API，本节专门介绍配置的同步。配置的同步涉及两个方面：第
            一，对原始的配置源实施监控并在其发生变化之后重新加载配置；第
            二，配置重新加载之后及时通知应用程序，进而使应用能够及时使用
            最新的配置。要了解配置同步机制的实现原理，需要先了解配置数据
            的流向
            */
            #region 6.4.1 配置数据流
            /*
                ConfigurationRoot对象保持着对所有注册的
            IConfigurationSource对象提供的 IConfiguration Provider对象的引
            用，当调用 ConfigurationRoot对象或者 ConfigurationSection对象相
            应的API提取配置数据时,最终都会直接从这些
            IConfigurationProvider 对象中提取数据。换句话说，配置数据在整
            个模型中只以配置字典的形式存储在IConfigurationProvider对象上
            面。
             */
            #endregion

            #region 6.4.2 ConfigurationReloadToken可以
            /*
                ConfigurationRoot类型和ConfigurationSection类型的
            GetReloadToken方法返回的IChangeToken对象都是Configurat
            ionReloadToken类型
             */
            #endregion

            #region 6.4.3 ConfigurationRoot
            /*
                下面介绍由 ConfigurationBuilder对象的 Build方法直接创建的ConfigurationRoot对象具有怎样的实现
                
                
             */
            #endregion
            #endregion

            #region 6.5 多样性的配置源
            #region 6.5.1 MemoryConfigurationSource（内存）
            /*
                利用MemoryConfigurationSource生成配置时，我们需要将其注
                册到IConfigurationBuilder对象之上。具体来说，我们可以像前面演
                示的实例一样直接调用IConfigurationBuilder接口的Add方法，也可
                以调用如下所示的两个重载的AddInMemoryCollection扩展方法
             */
            #endregion

            #region 6.5.2 EnvironmentVariablesConfigurationSource（环境变量）
            ///*
            //    环境变量的提取和维护可以通过静态类型Environment来完成。
            //    具体来说，我们可以调用它的静态方法 GetEnvironmentVariable 获
            //    得某个指定名称的环境变量的值，而 GetEnvironment Variables 方
            //    法则会返回所有的环境变量，EnvironmentVariableTarget 枚举类型
            //    的参数代表环境变量作用域决定的存储位置
            // */
            //Environment.SetEnvironmentVariable("TEST_GENDER", "Male");
            //Environment.SetEnvironmentVariable("TEST_AGE", "18");
            //Environment.SetEnvironmentVariable("TEST_CONTACTINFO:EMAILADDRESS", "foobar@outlook.com");
            //Environment.SetEnvironmentVariable("TEST_CONTACTINFO:PHONENO", "123456789");

            //var profile = new ConfigurationBuilder()
            //    .AddEnvironmentVariables("TEST_")
            //    .Build()
            //    .Get<Profile>();

            //Debug.Assert(profile.Equals(
            //        new Profile(Gender.Male, 18, "foobar@outlook.com", "123456789")));
            #endregion

            #region 6.5.3 CommandLineConfigurationSource(命令行)
            ///*
            //    在很多情况下，我们会采用 Self-Host方式将一个 ASP.NET Core
            //应用寄宿到一个托管进程中，此时我们倾向于采用命令行的方式来启
            //动寄宿程序。当以命令行的形式启动一个 ASP.NET Core 应用时，我
            //们希望直接使用命名行开关（Switch）来控制应用的一些行为，所以
            //命令行开关自然也就成了配置常用的来源之一。配置模型针对这种配
            //置源的支持是通过CommandLineConfigurationSource 实现的，该类
            //型定义在
            //NuGet 包“Microsoft.Extensions.Configuration.CommandLine”中
            //● {name}={value}.
            //● {prefix}{name}={value}.
            // */
            //try
            //{
            //    var mapping = new Dictionary<string, string>
            //    {
            //        ["-a"] = "architecture",
            //        ["-arch"] = "architecture"
            //    };
            //    var configuration = new ConfigurationBuilder()
            //        .AddCommandLine(args, mapping)
            //        .Build();
            //    Console.WriteLine($"Architecture:{configuration["architecture"]}");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error:{ex.Message}");
            //}
            #endregion

            #region 6.5.4 FileConfigurationSource（文件）
            /*
                物理文件是我们最常用到的原始配置载体，而最佳的配置文件格
                式主要有3种，即JSON、XML和INI，对应的配置源类型分别是
                JsonConfigurationSource、XmlConfigurationSource和IniConfigurationSource，它们具有如下一个相同的基类FileConfigurationSource
                1、JsonConfigurationSource
                2、XmlConfigurationSource
                3、InitConfigurationSource
             */


            var source = new FakeConfigurationSource()
            {
                Path = @"C:\App\appsettings.json"
            };
            Debug.Assert(source.FileProvider == null);

            source.ResolveFileProvider();
            var filePrivider = (PhysicalFileProvider)source.FileProvider;
            Debug.Assert(filePrivider.Root == @"C:\App\");
            Debug.Assert(source.Path == "appsettings.json");

            #endregion

            #region 6.5.5 StreamConfigurationSource
            /*
                StreamConfigurationSource 对象通过指定的 Stream 对象来读取
            配置内容，所以这种配置源具有更加灵活的应用。如下面的代码片段
            所示，StreamConfigurationSource 是一个抽象类，用于读取配置内
            容的输出流体现在它的Stream属性中

                如下面的代码片段所示，这些具体的 StreamConfigurationSource 类型通过重写的 Build 方法
            提供对应的IConfigurationProvider对象，然后由它们利用指定的Stream对象读取对应的JSON文件、
            XML文件和INI文本并转换成配置字典
             */
            #endregion

            #region 6.5.6 ChainedConfigurationSource
            //ChainedConfigurationSource
            //ChainedConfigurationProvider
            #endregion

            #region 6.5.7 自定义ConfigurationSource
            //var initialSettings = new Dictionary<string, string>
            //{
            //    ["Gender"] = "Male",
            //    ["Age"] = "18",
            //    ["ContactInfo:EmailAddress"] = "foobar@outlook.com",
            //    ["ContactInfo:PhoneNo"] = "123456789"
            //};

            //var prifile = new ConfigurationBuilder()
            //    .AddJsonFile("appSettins.json")
            //    .Build()
            //    .Get<Profile>();

            #endregion

            #endregion
            #endregion

            
            //Console.Read();



        }

        private class FakeConfigurationSource : FileConfigurationSource
        {
            public override IConfigurationProvider Build(IConfigurationBuilder builder)
                => throw new NotImplementedException();
        }
    }
}
