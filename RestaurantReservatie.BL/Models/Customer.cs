namespace RestaurantReservatie.BL.Models;

public class Customer {
    private string _name;
    private int _userId;
    public ContactInfo Contact { get; set; }
    public string Name {
        get => _name;
        set {
            if (string.IsNullOrWhiteSpace(value)) {
                throw new ArgumentException("Name cannot be empty");
            }

            _name = value;
        }
    }


    public int UserId { get; set; }

    public Customer(string name, int userId, ContactInfo contact) {
        Name = name;
        UserId = userId;
        Contact = contact;
    }

    public Customer(string name,  ContactInfo contact) {
        Name = name;
        Contact = contact;
    }
}