using System.Collections.Generic;
using bestdealpharma.com.Models;

namespace bestdealpharma.com.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
