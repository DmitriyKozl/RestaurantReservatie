using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Models;
using RestaurantReservatie.DL.Data;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Mapper; 

public class LocationMapper {
    
    
public static Location MapToDomain(Location_Data location) {
    try
    {
        return new Location(location.PostalCode, location.City, location.StreetName, location.HouseNumber, location.LocationId);
    }
    catch (Exception e)
    {
        throw new MapperException("Location MapToDomain", e);
    }    
}

public static Location_Data MapToDB(Location location, RestaurantReservatieContext context) {
    try
    {
        return new Location_Data(location.PostalCode, location.City, location.Street, location.HouseNumber);
    }
    catch (Exception e)
    {
        throw new MapperException("Location_Data MapToDB", e);
    }
}

}