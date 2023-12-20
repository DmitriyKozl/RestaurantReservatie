using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Models;
using RestaurantReservatie.DL.Data;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Mapper;

public class CustomerMapper {
    private readonly RestaurantReservatieContext _context;

    public CustomerMapper(RestaurantReservatieContext context) {
        _context = context;
    }


    public static Customer MapToDomain(Customer_Data customerdb) {
        try {
            return new Customer(customerdb.CustomerId, customerdb.Name, customerdb.Phone,
                LocationMapper.MapToDomain(customerdb.Location), customerdb.Email);
        }
        catch (Exception e) {
            throw new MapperException("MapToDomain", e);
        }
    }

    public static Customer_Data MapToDB(Customer customer, RestaurantReservatieContext context) {
        try {
            Customer_Data g = context.Customer.Find(customer.CustomerId);
            Location_Data l =
                context.Location
                    .FirstOrDefault(loc => loc.StreetName == customer.Location.Street
                                           && loc.HouseNumber == customer.Location.HouseNumber
                                           && loc.City == customer.Location.City
                                           && int.Parse(loc.PostalCode) == int.Parse(customer.Location.PostalCode)) ??
                throw new InvalidOperationException();
            if (g is not null) {
                g.Name = customer.Name;
                g.Email = customer.Email;
                g.Phone = customer.Number;
                g.Location = l;
                return g;
            }

            return new Customer_Data(customer.Name, customer.Email, customer.Number,
                LocationMapper.MapToDB(customer.Location, context));
        }
        catch (Exception ex) {
            throw new MapperException("MapToDB");
        }
    }
}