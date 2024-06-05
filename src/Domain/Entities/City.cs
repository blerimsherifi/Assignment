namespace Assignment.Domain.Entities;

public class City : BaseAuditableEntity
{
    public int CityID { get; set; }
    public required string CityName { get; set; }

    // Foreign key
    public int CountryID { get; set; }

    // Navigation property
    public Country Country { get; set; } = null!;
}
