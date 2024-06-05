namespace Assignment.Application.DTOs;

public class CityDto
{
    public int Id { get; init; }
    public string CityName { get; set; } = string.Empty;
    public int CountryID { get; set; }
    public string CountryName { get; set; } = string.Empty;
    public string Temperature { get; set; } = string.Empty;
}
