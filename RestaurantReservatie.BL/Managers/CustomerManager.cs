using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Managers; 

public class CustomerManager  {
    
    ICustomerRepository _customerRepository;
    
    public CustomerManager(ICustomerRepository customerRepository){
        _customerRepository = customerRepository;
    }
    
    public void AddCustomer(Customer customer){
        _customerRepository.AddCustomer(customer);
    }
    
    public List<Customer> GetCustomers(string filter){
        return _customerRepository.GetCustomers(filter);
    }
    
    public void DeleteCustomer(int id){
        _customerRepository.DeleteCustomer(id);
    }
    
    public void UpdateCustomer(Customer customer){
        _customerRepository.UpdateCustomer(customer);
    }
}