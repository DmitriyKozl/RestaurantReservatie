using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Interfaces; 

public interface ICustomerRepository {
    void UpdateCustomer(Customer customer);
    void DeleteCustomer(int id);
    List<Customer> GetCustomers(string filter);
    void AddCustomer(Customer customer); }
