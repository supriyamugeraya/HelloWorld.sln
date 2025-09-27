//using HelloWorld.Web.Controllers;
//using HelloWorld.Web.Services;
//using Microsoft.Extensions.Logging;
//using NSubstitute;
//using System.Threading.Tasks;
//using Xunit;

//namespace HelloWorld.Web.Tests.Controllers
//{
//    public class WeatherForecastControllerTests
//    {
//        private readonly ILogger<WeatherForecastController> _logger;
//        private readonly WeatherForecastController _sut;
//        private IWeatherForecastService _weatherForecastService;


//        private static readonly string[] Summaries = new[]
//        {
//            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//        };

//        private static readonly WeatherForecast[] WeatherForecasts
//            = Enumerable.Range(1, 5).Select(index => new WeatherForecast
//            {
//                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                TemperatureC = Random.Shared.Next(-20, 55),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();

//        public WeatherForecastControllerTests()
//        {
//            _logger = Substitute.For<ILogger<WeatherForecastController>>();
//            _weatherForecastService = Substitute.For<IWeatherForecastService>();
//            _sut = new WeatherForecastController(_logger, _weatherForecastService);
//        }

//        [Fact]
//        public async Task Get_FiveDayForecast_ReturnsFiveItems()
//        {
//            _weatherForecastService.GetData("W1").Returns(WeatherForecasts);
//            // Act

//            var result =await _sut.Get("W1");

//            //Assert

//            Console.WriteLine($"Result count: {result.Count()}");
//            Assert.Equal(5, result.Count());
//            //Assert.NotNull(result);


//        }
//        [Fact]
//        public async Task Get_FiveDayForecast_ReturnsNextFiveDaysData()
//        {

//            _weatherForecastService.GetData("W1").Returns(WeatherForecasts);

//            //Arrange
//            var dateNow = DateTime.Now;
//            var firstDay = dateNow.AddDays(1).Day;
//            var lastDay = dateNow.AddDays(5).Day;

//            //Act 
//            var result =await _sut.Get("W1");
//            Assert.Equal(firstDay, result.First().Date.Day);
//            Assert.Equal(lastDay, result.Last().Date.Day);
//        }

//        [Fact]
//        public void Get_LogInformation_MessageLoggedCorrectly()
//        {

//            _weatherForecastService.GetData("W1").Returns(WeatherForecasts);

//            await _sut.Get("W1");

//            // Act

//            // Assert
//            _logger
//                .Received(1)
//                .LogInformation("Get method was called");
//        }


//        [Fact]
//        public async Task Get_ReturnsMockedForecastData()
//        {
//            // Arrange
//            _weatherForecastService
//                .GetData("W1")
//                .Returns(WeatherForecasts);

//        }

//    }
//}
using HelloWorld.Web.Controllers;
using HelloWorld.Web.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace HelloWorld.Web.Tests.Controllers
{
    public class WeatherForecastControllerTests
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastController _sut;
        private readonly IWeatherForecastService _weatherForecastService;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly WeatherForecast[] WeatherForecasts
            = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

        public WeatherForecastControllerTests()
        {
            _logger = Substitute.For<ILogger<WeatherForecastController>>();
            _weatherForecastService = Substitute.For<IWeatherForecastService>();
            _sut = new WeatherForecastController(_logger, _weatherForecastService);
        }

        [Fact]
        public async Task Get_FiveDayForecast_ReturnsFiveItems()
        {
            _weatherForecastService.GetData("W1").Returns(WeatherForecasts);

            var result = await _sut.Get("W1");

            Console.WriteLine($"Result count: {result.Count()}");
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public async Task Get_FiveDayForecast_ReturnsNextFiveDaysData()
        {
            _weatherForecastService.GetData("W1").Returns(WeatherForecasts);

            var result = await _sut.Get("W1");

            var dateNow = DateTime.Now;
            var firstDay = dateNow.AddDays(1).Day;
            var lastDay = dateNow.AddDays(5).Day;

            Assert.Equal(firstDay, result.First().Date.Day);
            Assert.Equal(lastDay, result.Last().Date.Day);
        }

        [Fact]
        public async Task Get_LogInformation_MessageLoggedCorrectly()
        {
            _weatherForecastService.GetData("W1").Returns(WeatherForecasts);

            await _sut.Get("W1");

            _logger.Received(1).LogInformation("Get method was called");
        }

        [Fact]
        public async Task Get_ReturnsMockedForecastData()
        {
            _weatherForecastService.GetData("W1").Returns(WeatherForecasts);

            var result = await _sut.Get("W1");

            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
            //Assert.Equal("Freezing", result.First().Summary); // Optional: depends on mock data
        }

        [Fact]
        public async Task Get_NoDistrictCode_ThrowsException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _sut.Get(""));


            Assert.Equal("Your exception message", exception.Message);
        }
    }
}
