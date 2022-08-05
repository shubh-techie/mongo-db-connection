using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDbService;

namespace MongoDB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppsettingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<RoundTheCodeSync> _options;
        public AppsettingController(IConfiguration configuration, IOptions<RoundTheCodeSync> options)
        {
            _configuration = configuration;
            _options = options;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string firstWay = _configuration.GetSection("RoundTheCodeSync").GetChildren().FirstOrDefault(config => config.Key == "Title").Value;
            string otherWay = _configuration.GetValue<string>("RoundTheCodeSync:Title");
            return Content($"{firstWay + " " + otherWay}", "text/plain");
        }

        //[HttpGet("otherway")]
        //public IActionResult Get()
        //{
        //    string firstWay = _options.Value.Title;
        //    string otherWay = _options.Value.ConcurrentThreads;
        //    return Content($"{firstWay + " " + otherWay}", "text/plain");
        //}
    }
}
