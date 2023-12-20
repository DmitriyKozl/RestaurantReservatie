using RestaurantReservatie.BL.Exceptions;

namespace RestaurantReservatie.BL.Models;

public class Location {
    public int LocationId { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string HouseNumber { get; set; }


    public Location(string city, string postalCode) {
        SetCity(city);
        SetPostalCode(postalCode);
    }

    public Location(string postalCode, string city, string street, string houseNumber, int locationId) : this(
         city,postalCode) {
        Street = street;
        HouseNumber = houseNumber;
        LocationId = locationId;
    }

    public Location(string postalCode, string city, string street, string houseNumber) : this(
        city,postalCode) {
        Street = street;
        HouseNumber = houseNumber;
    }


    public void SetPostalCode(string postalcode) {
        if (string.IsNullOrWhiteSpace(postalcode))
            throw new LocationException("ZetPostcode - Postcode mag niet leeg zijn");
        PostalCode = postalcode;
    }

    public void SetCity(string city) {
        if (string.IsNullOrWhiteSpace(city))
            throw new LocationException("ZetGemeenteNaam - GemeenteNaam mag niet leeg zijn");
        City = city;
    }

    public override string ToString() {
        return $"{City} [{PostalCode}] - {Street} - {HouseNumber}";
    }
}