using Assignment.Application.DTOs;
using Assignment.Application.WeatherForecast.Queries.GetCities;
using Assignment.Application.WeatherForecast.Queries.GetCountries;
using Caliburn.Micro;
using MediatR;

namespace Assignment.UI;

public class WeatherForecastViewModel : Screen
{
    private readonly ISender _sender;

    private IList<CountryDto> countries;
    private IList<CityDto> cities;
    private CountryDto selectedCountry;
    private CityDto selectedCity;
    private string temperature;

    public IList<CountryDto> Countries
    {
        get => countries;
        set
        {
            countries = value;
            NotifyOfPropertyChange(() => Countries);
        }
    }

    public IList<CityDto> Cities
    {
        get => cities;
        set
        {
            cities = value;
            NotifyOfPropertyChange(() => Cities);
        }
    }

    public CountryDto SelectedCountry
    {
        get => selectedCountry;
        set
        {
            selectedCountry = value;
            NotifyOfPropertyChange(() => SelectedCountry);
            LoadCitiesAsync();
        }
    }

    public CityDto SelectedCity
    {
        get => selectedCity;
        set
        {
            selectedCity = value;
            NotifyOfPropertyChange(() => SelectedCity);
            LoadTemperature();
        }
    }

    public string Temperature
    {
        get => temperature;
        set { temperature = value; NotifyOfPropertyChange(() => Temperature); }
    }

    public WeatherForecastViewModel(ISender sender)
    {
        _sender = sender;
        LoadCountriesAsync();
    }

    private async void LoadCountriesAsync()
    {
        Countries = await _sender.Send(new GetCountriesQuery());
    }

    private async void LoadCitiesAsync()
    {
        if (SelectedCountry != null)
        {
            Cities = await _sender.Send(new GetCitiesQuery { CountryID = SelectedCountry.Id });
        }
        else
        {
            Cities.Clear();
        }
    }

    private async void LoadTemperature()
    {
        if (SelectedCity != null)
        {
            Temperature = await GetTemperatureAsync(SelectedCity.CountryID);
        }
        else
        {
            Temperature = string.Empty;
        }

        static Task<string> GetTemperatureAsync(int cityId)
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
}
