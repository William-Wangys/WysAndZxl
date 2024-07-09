using DI.Interface;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DI.Middleware
{
    public class FoobarMiddleware
    {
        public readonly RequestDelegate _next;

        public FoobarMiddleware(RequestDelegate next) => _next = next;

        public Task InvokeAsync(HttpContext httpContext, IFoo foo, IBar bar, IBaz baz) 
        {
            return Task.CompletedTask;
        }
    }
}
