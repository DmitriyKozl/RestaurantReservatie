using System.Text.RegularExpressions;
using RestaurantReservatie.BL.Exceptions;

namespace RestaurantReservatie.BL.Models;

public class Customer {
    public string Email { get; set; }
    public string Number { get; set; }

    public string Name { get; set; }

    public int CustomerId { get; set; }

    public Location Location { get; set; }

    public Customer(int customerId, string name, string number, Location location, string email) {
        ZetNaam(name);
        ZetId(customerId);
        ZetEmail(email);
        ZetTelefoonnummer(number);
        Location = location;
    }

    public Customer(string name, string number, Location location, string email) {
        ZetNaam(name);
        ZetEmail(email);
        ZetTelefoonnummer(number);
        Location = location;
    }

    public Customer() { }
    //Naam (mag niet leeg zijn)
    public void ZetNaam(string name) {
        if (string.IsNullOrWhiteSpace(name)) throw new CustomerException("ZetNaam - Naam mag niet leeg zijn");
        Name = name;
    }
//ls de gebruiker zich heeft geregistreerd dan ontvangt hij een klantnummer (numeriek en groter dan 0)
    public void ZetId(int id) {
        if (id <= 0) throw new CustomerException("ZetId - Id < 0");
        CustomerId = id;
    }
//Email (basis check uitvoeren)
    public void ZetEmail(string email) {
        if (string.IsNullOrWhiteSpace(email)) throw new CustomerException("ZetEmail - Email mag niet leeg zijn");
        if (!Regex.IsMatch(email,
                @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            throw new CustomerException(
                "ZetEmail - Email is niet geldig");
        Email = email;
    }

    public void ZetTelefoonnummer(string number) {
        if (string.IsNullOrWhiteSpace(number))
            throw new CustomerException("ZetTelefoonnummer - Telefoonnummer mag niet leeg zijn");
        if (!Regex.IsMatch(number, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"))
            throw new CustomerException("ZetTelefoonnummer - Telefoonnummer is niet geldig");
        Number = number;
    }
}