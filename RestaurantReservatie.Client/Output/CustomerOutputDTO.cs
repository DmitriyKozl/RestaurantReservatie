namespace TestEFLayer.Output;

public class CustomerOutputDTO
{
    public CustomerOutputDTO() { }
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public LocationOutputDTO Location { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Email: {Email}, Phone: {Phone}, Location: {Location.street} {Location.housenumber}  {Location.postalcode} {Location.municipality}";
    }
}