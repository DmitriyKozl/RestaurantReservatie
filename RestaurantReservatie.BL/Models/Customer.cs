using System.Text.RegularExpressions;
using RestaurantReservatie.BL.Exceptions;

namespace RestaurantReservatie.BL.Models;

public class Customer
{
    public string Email { get; set; }
    public string Number { get; set; }

    public string Name { get; set; }

    public int CustomerId { get; set; }

    public Location Location { get; set; }

    public Customer(int customerId, string name, string number, Location location, string email)
    {
        SetName(name);
        SetId(customerId);
        SetEmail(email);
        SetPhone(number);
        Location = location;
    }

    public Customer(string name, string number, Location location, string email)
    {
        SetName(name);
        SetEmail(email);
        SetPhone(number);
        Location = location;
    }

    public Customer()
    {
    }

    //Naam (mag niet leeg zijn)
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new CustomerException("SetName - Naam mag niet leeg zijn");
        Name = name;
    }

//ls de gebruiker zich heeft geregistreerd dan ontvangt hij een klantnummer (numeriek en groter dan 0)
    public void SetId(int id)
    {
        if (id <= 0) throw new CustomerException("SetId - Id < 0");
        CustomerId = id;
    }

//Email (basis check uitvoeren)
    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new CustomerException("SetEmail - Email mag niet leeg zijn");
        if (!Regex.IsMatch(email,
                @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            throw new CustomerException(
                "SetEmail - Email is niet geldig");
        Email = email;
    }

    public void SetPhone(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new CustomerException("SetPhone - Telefoonnummer mag niet leeg zijn");
        Number = number;
    }
}