using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Interfaces;

public interface IRestaurantRepository {
    #region Restaurant

    Restaurant AddRestaurant(Restaurant restaurant);
    List<Restaurant> GetAllRestaurants();
    Restaurant GetRestaurants(int id);
    
    Restaurant GetRestaurantByName(string name);
    void DeleteRestaurant(int id);
    Restaurant UpdateRestaurant(Restaurant restaurant);

    List<Restaurant> GetRestaurantByCuisine(string cuisine);
    List<Restaurant> GetRestaurantByLocation(string city);
    bool RestaurantExists(int id);
    bool SameRestaurant(Restaurant restaurant);

    #endregion

    #region Table

    Table AddTable(Table table);
    Table UpdateTable(int restaurantId, Table table);
    Table DeleteTable(int restaurantId, int tableId);
    List<Table> GetOpenTables(DateTime datum, Location locatie, string keuken);
    List<Table> GetTables(int restaurantId);
    Table GetTable(int TableId, int restaurantId);

    List<Table> GetRestaurantTables(Restaurant restaurant);
    List<Table> GetRestaurantOpenTables(Restaurant restaurant);
    List<Table> GetTableByCapacity(int restaurantId, DateTime date, int capacity);

    bool TableExists(Table table);
    bool SameTable(Table table);

    #endregion

    #region Reservation

    List<Reservation> GetReservationsByDate(int id, DateTime date);
    List<Restaurant> GetRestaurantsAvailabilityByDate(DateTime date, int numSeats);

    #endregion
}