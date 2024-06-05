namespace Assignment.Domain.Entities;

public class Country : BaseAuditableEntity
{
    public int CountryID { get; set; }
    public required string CountryName { get; set; }

    // Navigation property to represent the one-to-many relationship
    public ICollection<City>? Cities { get; set; }
}
