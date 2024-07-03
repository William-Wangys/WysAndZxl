using App.Interface;
using App.Options;
using Microsoft.Extensions.Options;

namespace App.Service
{
    public class Greeter: IGreeter
    {
        #region 配置选项
        #region 1.IConfiguration
        //private readonly IConfiguration _configuration;
        //public Greeter(IConfiguration configuration)
        //    => _configuration = configuration.GetSection("greeting");
        //public string Greet(DateTimeOffset time) => time.Hour switch
        //{
        //    var h when h >= 5 && h < 12 => _configuration["morning"],
        //    var h when h >= 12 && h < 17 => _configuration["afternoon"],
        //    _ => _configuration["evening"]
        //};
        #endregion

        #region 2.IOptions
        //private readonly GreetingOptions _options;
        //public Greeter(IOptions<GreetingOptions> optionsAccessor)
        //    => _options = optionsAccessor.Value;

        //public string Greet(DateTimeOffset time) => time.Hour switch
        //{
        //    var h when h >= 5 && h < 12 => _options.Morning,
        //    var h when h >= 12 && h < 17 => _options.Afternoon,
        //    _ => _options.Evening
        //};
        #endregion
        #endregion

        #region 诊断日志
        private readonly GreetingOptions _options;
        private readonly ILogger _logger;

        public Greeter(IOptions<GreetingOptions> optionsAccessor, ILogger<Greeter> logger)
        {
            _options = optionsAccessor.Value;
            _logger = logger;
        }

        public string Greet(DateTimeOffset time)
        {
            var message = time.Hour switch
            {
                var h when h >= 5 && h < 12     => _options.Morning,
                var h when h >= 12 && h < 17    => _options.Afternoon,
                _                               => _options.Evening
            };
            _logger.LogInformation(message: $"{time} => {message}");
            return message;
        }
        #endregion
    }
}
