using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Interfaces;

public interface ICustomerRepository {
    Customer UpdateCustomer(Customer customer);
    void DeleteCustomer(int id);
    Customer GetCustomer(int id);

    List<Customer> GetAllCustomers();
    Customer AddCustomer(Customer customer);
    bool CustomerExists(int id);
    bool CustomerHasReservation(int customerId);
}