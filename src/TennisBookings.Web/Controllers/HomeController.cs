using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Services;
using TennisBookings.Web.ViewModels;

namespace TennisBookings.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherForecaster _weatherForecaster;
        private readonly FeaturesConfiguration _featuresConfiguration;
        private readonly GuidService _guidService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IWeatherForecaster weatherForecaster,
            IOptions<FeaturesConfiguration> options,
            GuidService guidService,
            ILogger<HomeController> logger)
        {
            _weatherForecaster = weatherForecaster;
            _featuresConfiguration = options.Value;
            _guidService = guidService;
            _logger = logger;
        }

        [Route("")]
        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();

            var currentWeather = _weatherForecaster.GetCurrentWeather();

            var guid = _guidService.GetGuid();
            var logMessage = $"Controller: The GUID from GuidServie is {guid}";
            
            _logger.LogInformation(logMessage);
            
            var featuresConfigurationEnableWeatherForecast = _featuresConfiguration.EnableWeatherForecast;

            switch (currentWeather.WeatherCondition)
            {
                case WeatherCondition.Sun:
                    viewModel.WeatherDescription = "It's sunny right now. " +
                                                   "A great day for tennis.";
                    break;
                case WeatherCondition.Rain:
                    viewModel.WeatherDescription = "We're sorry but it's raining " +
                                                   "here. No outdoor courts in use.";
                    break;
                default:
                    viewModel.WeatherDescription = "We don't have the latest weather " +
                                                   "information right now, please check again later.";
                    break;
            }

            return View(viewModel);
        }
    }
}