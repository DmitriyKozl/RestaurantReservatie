using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;
using RestaurantReservatie.DL.Data;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Repositories; 

public class CustomerRepository : ICustomerRepository {



    private readonly RestaurantReservatieContext _context;
    public CustomerRepository(RestaurantReservatieContext context) {
        
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CreateCustomer(Customer customer) {

    }


    public void UpdateCustomer(Customer customer) {
        throw new NotImplementedException();
    }

    public void DeleteCustomer(int id) {
        throw new NotImplementedException();
    }

    public List<Customer> GetCustomers(string filter) {
        throw new NotImplementedException();
    }

    public void AddCustomer(Customer customer) {
        throw new NotImplementedException();
    }
}