namespace HelloWorld.Web.Services
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetData(string districtCode);
    }
}
