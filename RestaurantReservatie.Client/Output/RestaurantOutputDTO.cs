namespace RestaurantReservatie.Client.Output;

public class RestaurantOutputDTO
{
    public RestaurantOutputDTO() { }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public string Cuisine { get; set; }
    public LocationOutputDTO Location { get; set; }
    public bool IsActive { get; set; }

    public override string ToString()
    {
        return $"Id:{Id} Name: {Name}, Email: {Email}, Phone: {Phone}, Cuisine: {Cuisine} Location: {Location.street} {Location.housenumber}  {Location.postalcode} {Location.municipality}, IsActive: {IsActive}";
    }
}