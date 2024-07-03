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
using App;
using App.Interface;
using App.Middleware;
using App.Options;
using App.Service;


//调 用 IServiceCollection 对 象 的 Configure<TOptions> 扩 展 方 法 来 完 成
var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IGreeter, Greeter>()
    .Configure<GreetingOptions>(builder.Configuration.GetSection("greeting"));
var app = builder.Build();
app.UseMiddleware<GreetingMiddleware>();
app.Run();
#endregion

#region 诊断日志
//诊断日志：在演示程序中，Greeter 类型会根据指定的时间返回对应的问候语，现在将时间和对应的问候语以日志的方式记录下来并看一看两者是否匹配
#endregion
