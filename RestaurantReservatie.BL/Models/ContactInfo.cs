using RestaurantReservatie.BL.Exceptions;

namespace RestaurantReservatie.BL.Models;

public class ContactInfo {
    private string _email;
    private string _phoneNumber;
    public Location Location { get; set; }


    public string Email {
        get { return _email; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new ContactInfoException("Email cannot be empty");
            _email = value;
        }
    }

    public string PhoneNumber {
        get { return _phoneNumber; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new ContactInfoException("Phone number cannot be empty");
            _phoneNumber = value;
        }
    }


    public ContactInfo(string email, string phoneNumber, Location location) {
        Email = email;
        PhoneNumber = phoneNumber;
        Location = location;
    }
}