using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Controllers
{
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private readonly GuidService _guidService;
        private readonly ILogger<HomeController> _logger;

        public HomeApiController(GuidService guidService, ILogger<HomeController> logger)
        {
            _guidService = guidService;
            _logger = logger;
        }

        [Route("/homeapi")]
        public IActionResult Index()
        {
            var guid = _guidService.GetGuid();

            var logMessage = $"Controller: The GUID from GuidService is {guid}";

            _logger.LogInformation(logMessage);

            var messages = new List<string>
            {
                HttpContext.Items["MiddlewareGuid"].ToString(),
                logMessage
            };
            
            return Ok(messages);
        }
    }
}