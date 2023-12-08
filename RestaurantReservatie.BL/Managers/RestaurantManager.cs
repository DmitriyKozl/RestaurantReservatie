using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Managers; 

public class RestaurantManager {
    
    IRestaurantRepository _restaurantRepository;
    
    public RestaurantManager(IRestaurantRepository restaurantRepository){
        _restaurantRepository = restaurantRepository;
    }
    
    public void AddRestaurant(Restaurant restaurant){
        _restaurantRepository.AddRestaurant(restaurant);
    }
    
    public List<Restaurant> GetRestaurants(string filter){
        return _restaurantRepository.GetRestaurants(filter);
    }
    
    public void DeleteRestaurant(int id){
        _restaurantRepository.DeleteRestaurant(id);
    }
    
    public void UpdateRestaurant(Restaurant restaurant){
        _restaurantRepository.UpdateRestaurant(restaurant);
    }
    

    public List<Restaurant> SearchRestaurants(string? location, string? cuisine){
        return _restaurantRepository.SearchRestaurants(location, cuisine);
    }
    
    
    
    
}