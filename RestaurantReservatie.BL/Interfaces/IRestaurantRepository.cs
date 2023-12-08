using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Interfaces; 

public interface IRestaurantRepository {
    
    void AddRestaurant(Restaurant restaurant);
    
    List<Restaurant> GetRestaurants(string filter);
    void DeleteRestaurant(int id);
    void UpdateRestaurant(Restaurant restaurant);
    List<Restaurant> SearchRestaurants(string? location, string? cuisine);
}