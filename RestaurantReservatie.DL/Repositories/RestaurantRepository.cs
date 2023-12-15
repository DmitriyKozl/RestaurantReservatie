using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.DL.Repositories; 

public class RestaurantRepository : IRestaurantRepository {
    public void AddRestaurant(Restaurant restaurant) {
        throw new NotImplementedException();
    }

    public List<Restaurant> GetRestaurants(string filter) {
        throw new NotImplementedException();
    }

    public void DeleteRestaurant(int id) {
        throw new NotImplementedException();
    }

    public void UpdateRestaurant(Restaurant restaurant) {
        throw new NotImplementedException();
    }

    public List<Restaurant> SearchRestaurants(string? location, string? cuisine) {
        throw new NotImplementedException();
    }
}