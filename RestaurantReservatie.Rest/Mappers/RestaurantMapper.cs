using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Models;
using RestaurantReservatie.Rest.Models.Output;

namespace RestaurantReservatie.Rest.Mappers;

public class RestaurantMapper {
    public static RestaurantOutputDTO MapFromDomain(Restaurant restaurant) {
        try {
            return new RestaurantOutputDTO(
                restaurant.RestaurantId,
                restaurant.RestaurantName,
                restaurant.Location,
                restaurant.Cuisine,
                restaurant.Phone,
                restaurant.Email);
        }
        catch (Exception e) {
            throw new MapperException("MapFromDomain", e);
        }
    }

    public static Restaurant MapToDomain(RestaurantOutputDTO restaurant) {
        try {
            return new Restaurant(
                restaurant.Id,
                restaurant.Name,
                restaurant.Location,
                restaurant.Cuisine,
                restaurant.Phone,
                restaurant.Email,
                restaurant.Tables
            );
        }
        catch (Exception e) {
            throw new MapperException("MapToDomain", e);
        }
    }
}