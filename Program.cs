//var app = WebApplication.Create(args);
//IApplicationBuilder appBuilder = app;
//appBuilder
//    .Use(middleware: HelloMiddleware)
//    .Use(middleware: WorldMiddleware);
//app.Run();

//static RequestDelegate HelloMiddleware(RequestDelegate next)
//=> async httpContext => {
//    await httpContext.Response.WriteAsync("Hello， ");
//    await next(httpContext);
//};
//static RequestDelegate WorldMiddleware(RequestDelegate next)
//=> httpContext => httpContext.Response.WriteAsync("World!");


//static async Task HelloMiddleware(HttpContext httpContext, RequestDelegate next)
//{
//    await httpContext.Response.WriteAsync("Hello, ");
//    await next(httpContext);
//};

//static Task WorldMiddleware(HttpContext httpContext, RequestDelegate next)
//    => httpContext.Response.WriteAsync("World!");

//static async Task HelloMiddleware(HttpContext httpContext, Func<Task> next)
//{
//    await httpContext.Response.WriteAsync("Hello, ");
//    await next();
//}

//static Task WorldMiddleware(HttpContext httpContext, Func<Task> next)
//=> httpContext.Response.WriteAsync("World!");


#region 中间件
//强类型中间件
//using App;
//using App.Interface;
//using App.Middleware;
//using App.Service;


//var builder = WebApplication.CreateBuilder(args);
//builder.Services
//    .AddSingleton<IGreeter, Greeter>()
//    .AddSingleton<GreetingMiddleware>();
//var app = builder.Build();
//app.UseMiddleware<GreetingMiddleware>();
//app.Run();

//自定义中间件
//using App;
//using App.Interface;
//using App.Middleware;
//using App.Service;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<IGreeter, Greeter>();
//var app = builder.Build();
//app.UseMiddleware<GreetingMiddleware>();
//app.Run();
#endregion

#region 配置选项 
//using App;
//using App.Interface;
//using App.Middleware;
//using App.Options;
//using App.Service;


////调 用 IServiceCollection 对 象 的 Configure<TOptions> 扩 展 方 法 来 完 成
//var builder = WebApplication.CreateBuilder(args);
//builder.Services
//    .AddSingleton<IGreeter, Greeter>()
//    .Configure<GreetingOptions>(builder.Configuration.GetSection("greeting"));
//var app = builder.Build();
//app.UseMiddleware<GreetingMiddleware>();
//app.Run();
#endregion

#region 诊断日志
////诊断日志：在演示程序中，Greeter 类型会根据指定的时间返回对应的问候语，现在将时间和对应的问候语以日志的方式记录下来并看一看两者是否匹配
#endregion

#region 路由
////ASP.NET Core 的路由是由 EndpointRoutingMiddleware 和 EndpointMiddleware 两个中间件实
////现的，在所有预定义的中间件类中，它们是比较重要的两个中间件，因为不仅是 MVC 和 gRPC
////框架建立在路由系统之上，后面介绍的 Dapr.NET 的发布订阅和 Actor 编程模式也是如此。我们
////会在“第 20 章 路由”中系统地介绍由这两个中间件构建的路由系统。现在我们放弃前面演示
////实例中注册的中间件，改用路由的方式来实现类似的功能

///*我 们 在 利 用 WebApplicationBuilder 将 表 示 承 载 应 用 的
//  WebApplication 对象构建出来之后，并没有注册任何中间件，而是调用它的 MapGet 扩展方法注
//  册了一个指向路径“/ greet”的路由终节点（Endpoint）
//*/
//using App;
//using App.Interface;
//using App.Options;
//using App.Service;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services
//.AddSingleton<IGreeter, Greeter>()
//.Configure<GreetingOptions>(builder.Configuration.GetSection("greeting"));
//var app = builder.Build();
//app.MapGet("/greet", Greet);
//app.Run();

//static string Greet(IGreeter greeter) => greeter.Greet(DateTimeOffset.Now);
#endregion

#region MVC

//1.3.1 定义 Controller
using App;
using App.Interface;
using App.Options;
using App.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services
.AddSingleton<IGreeter, Greeter>()
.Configure<GreetingOptions>(builder.Configuration.GetSection("greeting"))
//.AddControllers();
//1.3.2 引入视图
.AddControllersWithViews();

var app = builder.Build();
app.MapControllers();
app.Run();
#endregion

#region gRPC
//gRPC 最开始由 Google（gRPC 的“g”）开发，目前已经成为一款高性能、开源、语言中立的远程调用（RPC）框架
#endregion
