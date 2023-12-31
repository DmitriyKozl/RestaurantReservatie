namespace TestEFLayer.Output;

public class LocationOutputDTO
{
    public LocationOutputDTO() { }
    public int locationId { get; set; }
    public int postalcode { get; set; }
    public string municipality { get; set; }
    public string street { get; set; }
    public string housenumber { get; set; }
}