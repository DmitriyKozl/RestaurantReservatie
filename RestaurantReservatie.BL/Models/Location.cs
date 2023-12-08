using RestaurantReservatie.BL.Exceptions;

namespace RestaurantReservatie.BL.Models;

public class Location {
    private const char splitChar = '|';

    public Location(string addressLine) {
        string[] parts = addressLine.Split(splitChar);
        City = parts[0];
        Street = parts[2];
        PostalCode = parts[1];
        HouseNumber = parts[3];
    }

    public Location(string city, string street, string postalCode, string houseNumber) {
        City = city;
        Street = street;
        PostalCode = postalCode;
        HouseNumber = houseNumber;
    }

    private string _city;

    public string City {
        get { return _city; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new LocationException("City can not be empty");
            _city = value;
        }
    }

    private string _postalCode;

    public string PostalCode {
        get => _postalCode;
        set {
            if (string.IsNullOrWhiteSpace(value)) {
                throw new LocationException("Postal code cannot be empty");
            }

            if (value.Length != 4) {
                throw new LocationException("Postal code must be 4 characters long");
            }

            _postalCode = value;
        }
    }

    private string _houseNumber;

    public string HouseNumber {
        get { return _houseNumber; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new LocationException("HouseNumber is empty");
            _houseNumber = value;
        }
    }

    private string _street;

    public string Street {
        get { return _street; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new LocationException("Street is empty");
            _street = value;
        }
    }

    public override string ToString() {
        return $"{City} [{PostalCode}] - {Street} - {HouseNumber}";
    }

    public string ToAddressLine() {
        return $"{City}{splitChar}{PostalCode}{splitChar}{Street}{splitChar}{HouseNumber}";
    }
}