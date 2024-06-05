namespace Assignment.Application.MockData;

public class MockWeatherService : IWeatherService
{
    public Task<string> GetTemperatureAsync(int cityId)
    {
        // Mock temperature data
        var temperature = cityId switch
        {
            1 => "20°C",
            2 => "25°C",
            3 => "15°C",
            4 => "18°C",
            5 => "22°C",
            6 => "28°C",
            _ => "Unknown"
        };

        return Task.FromResult(temperature);
    }
}
