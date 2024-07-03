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
using App;
using App.Interface;
using App.Middleware;
using App.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IGreeter, Greeter>();
var app = builder.Build();
app.UseMiddleware<GreetingMiddleware>();
app.Run();
#endregion
