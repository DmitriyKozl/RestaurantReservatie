using System.Formats.Asn1;
using RestaurantReservatie.BL.Exceptions;

namespace RestaurantReservatie.BL.Models;

public class Restaurant {
//View available tables for a specific date, optionally with location and cuisine.

public Restaurant(string name, string cuisine, string description, ContactInfo contactInfo, int tables) {
        Name = name;
        Description = description;
        Cuisine = cuisine;
        ContactInfo = contactInfo;
        Tables = tables;
    }
    

    public Restaurant(int restaurantId, string name, string cuisine, string description, ContactInfo contactInfo, int tables)
        : this(name, cuisine, description, contactInfo, tables) {
        RestaurantId = restaurantId;
    }

    private string _cuisine;
    private int _restaurantId;
    private string _name;
    private string _description;
    private ContactInfo _contactInfo;
    private int _tables;
    
    public int Tables {
        get { return _tables; }
        set {
            if (value <= 0) throw new RestaurantException("invalid tables");
            _tables = value;
        }
    }

    public string Cuisine {
        get { return _cuisine; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new RestaurantException("cuisine is empty");
            _cuisine = value;
        }
    }

    public int RestaurantId {
        get { return _restaurantId; }
        set {
            if (value <= 0) throw new RestaurantException("invalid id");
            _restaurantId = value;
        }
    }

    public string Name {
        get { return _name; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new RestaurantException("name is empty");
            _name = value;
        }
    }

    public string Description {
        get { return _description; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new RestaurantException("description is empty");
            _description = value;
        }
    }


    public ContactInfo ContactInfo { get; set; }

    public override string ToString() {
        return $"{RestaurantId}: {Name}, Description: {Description}, Location: {ContactInfo}";
    }

    public override bool Equals(object obj) {
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false;
        var restaurant = (Restaurant)obj;
        return restaurant.RestaurantId == RestaurantId;
    }
}