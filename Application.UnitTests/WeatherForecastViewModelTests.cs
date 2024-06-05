using Assignment.Application.DTOs;
using Assignment.Application.WeatherForecast.Queries.GetCities;
using Assignment.Application.WeatherForecast.Queries.GetCountries;
using Assignment.UI;
using MediatR;
using Moq;
using Xunit;

namespace Application.UnitTests;

public class WeatherForecastViewModelTests
{
    private readonly Mock<ISender> _senderMock;

    public WeatherForecastViewModelTests()
    {
        _senderMock = new Mock<ISender>();
    }

    [Fact]
    public async Task LoadCountriesAsync_ShouldSetCountries()
    {
        // Arrange
        var countries = new List<CountryDto>
        {
            new CountryDto { Id = 1, CountryName = "Country1" },
            new CountryDto { Id = 2, CountryName = "Country2" }
        };
        _senderMock.Setup(s => s.Send(It.IsAny<GetCountriesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(countries);

        var viewModel = new WeatherForecastViewModel(_senderMock.Object);


        // Act
        await CommonUtilities.InvokePrivateMethodAsync(viewModel, "LoadCountriesAsync");

        // Assert
        Assert.Equal(countries, viewModel.Countries);
    }

    [Fact]
    public void SelectedCountry_ShouldLoadCities()
    {
        // Arrange
        var cities = new List<CityDto>
        {
            new CityDto { Id = 1, CityName = "City1", CountryID = 1 },
            new CityDto { Id = 2, CityName = "City2", CountryID = 1 }
        };
        var selectedCountry = new CountryDto { Id = 1, CountryName = "Country1" };

        _senderMock.Setup(s => s.Send(It.IsAny<GetCitiesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(cities);

        var viewModel = new WeatherForecastViewModel(_senderMock.Object);

        // Act
        viewModel.SelectedCountry = selectedCountry;

        // Assert
        Assert.Equal(cities, viewModel.Cities);
    }

    [Fact]
    public void SelectedCity_ShouldLoadTemperature()
    {
        // Arrange
        var selectedCity = new CityDto { Id = 1, CityName = "City1", CountryID = 1 };

        var viewModel = new WeatherForecastViewModel(_senderMock.Object);

        // Act
        viewModel.SelectedCity = selectedCity;

        // Assert
        Assert.Equal("20°C", viewModel.Temperature);
    }
}
