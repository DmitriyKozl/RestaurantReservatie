using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Models;
using RestaurantReservatie.DL.Data;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Mapper;

public class RestaurantMapper {
    public static Restaurant MapToDomain(Restaurant_Data restaurantData) {
        try {
            return new Restaurant(restaurantData.RestaurantID,
                restaurantData.RestaurantName,
                LocationMapper
                    .MapToDomain(restaurantData.Location),
                restaurantData.Cuisine,
                restaurantData.PhoneNumber,
                restaurantData.Email);


        }
        catch (Exception e) {
            throw new MapperException("MapToDomain", e);
        }
    }

    public static Restaurant_Data MapToDB(Restaurant restaurant, RestaurantReservatieContext context) {
        try {
            Restaurant_Data r = context.Restaurant.Find(restaurant.RestaurantId);
            Location_Data l = context.Location.FirstOrDefault(loc => loc.StreetName == restaurant.Location.Street
                                                                     && loc.HouseNumber ==
                                                                     restaurant.Location.HouseNumber
                                                                     && loc.City == restaurant.Location.City
                                                                     && loc.PostalCode ==
                                                                     restaurant.Location.PostalCode) ?? LocationMapper.MapToDB(restaurant.Location, context);
            if (r != null) {
                r.RestaurantName = restaurant.RestaurantName;
                r.Location = l;
                r.Cuisine = restaurant.Cuisine;
                r.PhoneNumber = restaurant.Phone;
                r.Email = restaurant.Email;
                return r;
            }


            return new Restaurant_Data(restaurant.RestaurantName, LocationMapper.MapToDB(restaurant.Location, context),
                restaurant.Cuisine, restaurant.Email,
                restaurant.Phone);
        }
        catch (Exception e) {
            throw new MapperException("MapToDB", e);
        }
    }
}