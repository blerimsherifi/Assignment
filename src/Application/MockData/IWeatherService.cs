namespace Assignment.Application.MockData;

public interface IWeatherService
{
    Task<string> GetTemperatureAsync(int cityId);
}
