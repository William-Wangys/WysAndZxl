using App.Interface;
using Microsoft.AspNetCore.Http;

namespace App.Middleware
{
    public class GreetingMiddleware
    {
        #region 强类型中间件
        //public class GreetingMiddleware : IMiddleware
        //{
        //    private readonly IGreeter _greeter;
        //    public GreetingMiddleware(IGreeter greeter)
        //    => _greeter = greeter;
        //    public Task InvokeAsync(HttpContext context, RequestDelegate next)
        //    => context.Response.WriteAsync(_greeter.Greet(DateTimeOffset.Now));
        //}
        #endregion

        #region 自定义中间件
        private readonly IGreeter _greeter;
        public GreetingMiddleware(RequestDelegate next, IGreeter greeter)
            => _greeter = greeter;

        public Task InvokeAsync(HttpContext context)
            => context.Response.WriteAsync(_greeter.Greet(DateTimeOffset.Now));
        #endregion


        #region 定义的中间件类型，不一定非要注入构造函数中，可以直接注入到方法中
        //public GreetingMiddleware() { }

        //public Task Invoke(HttpContext context, IGreeter greeter)
        //    => context.Response.WriteAsync(greeter.Greet(DateTimeOffset.Now));
        #endregion

    }
}
