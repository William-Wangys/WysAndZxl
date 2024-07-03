using App.Interface;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class GreetingController: Controller
    {
        [HttpGet("/greet")]
        public string Greet([FromServices] IGreeter greeter)
            => greeter.Greet(DateTimeOffset.Now);
    }
}
