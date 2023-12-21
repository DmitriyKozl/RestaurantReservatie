using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Managers;

public class CustomerManager {
    ICustomerRepository _customerRepository;

    public CustomerManager(ICustomerRepository customerRepository) {
        _customerRepository = customerRepository;
    }

    public Customer AddCustomer(Customer customer) {
        try {
            if (customer == null) throw new CustomerManagerException("AddCustomer - Gebruiker mag niet null zijn");
            if (_customerRepository.CustomerExists(customer.CustomerId))
                throw new CustomerManagerException("AddCustomer - Gebruiker bestaat al");
            return _customerRepository.AddCustomer(customer);
        }
        catch (Exception ex) {
            throw new CustomerManagerException("AddCustomer - Er is een fout opgetreden", ex);
        }
    }

    public Customer GetCustomer(int id) {
        try {
            if (id < 0) throw new CustomerManagerException("GetCustomer - GebruikerId mag niet kleiner dan 0 zijn");
            return _customerRepository.GetCustomer(id);
        }
        catch (Exception ex) {
            throw new CustomerManagerException("GetCustomer - Er is een fout opgetreden", ex);
        }
    }

    public List<Customer> GetAllCustomers() {
        try {
            return _customerRepository.GetAllCustomers();
        }
        catch (Exception ex) {
            throw new CustomerManagerException("GetAllCustomers - Er is een fout opgetreden", ex);
        }
    }

    public void DeleteCustomer(int id) {
        try {
            if (id == null) throw new CustomerManagerException("DeleteCustomer - Gebruiker mag niet null zijn");
            if (!_customerRepository.CustomerExists(id))
                throw new CustomerManagerException("VerwijderGebruiker - Gebruiker bestaat niet");
            if (_customerRepository.CustomerHasReservation(id))
                throw new CustomerManagerException("DeleteCustomer - Gebruiker heeft nog reservaties");
            _customerRepository.DeleteCustomer(id);
        }
        catch (Exception ex) {
            throw new CustomerManagerException("DeleteCustomer - Er is een fout opgetreden", ex);
        }
    }

    public Customer UpdateCustomer(Customer customer) {
        try {
            if (customer == null) throw new CustomerManagerException("UpdateGebruiker - Gebruiker mag niet null zijn");
            if (!_customerRepository.CustomerExists(customer.CustomerId))
                throw new CustomerManagerException("UpdateGebruiker - Gebruiker bestaat niet");
            return _customerRepository.UpdateCustomer(customer);
        }
        catch (Exception ex) {
            throw new CustomerManagerException("UpdateCustomer - Er is een fout opgetreden", ex);
        }
    }

    public bool CustomerExists(int id) {
        try {
            if (id < 0) throw new CustomerManagerException("CustomerExists - Id mag niet kleiner dan 0 zijn");
            return _customerRepository.CustomerExists(id);
        }
        catch (Exception ex) {
            throw new CustomerManagerException("CustomerExists - Er is een fout opgetreden", ex);
        }
    }

    public bool CustomerHasReservation(int customerId) {
        try {
            if (customerId < 0)
                throw new CustomerManagerException("CustomerHasReservation - Id mag niet kleiner dan 0 zijn");
            return _customerRepository.CustomerHasReservation(customerId);
        }
        catch (Exception ex) {
            throw new CustomerManagerException("CustomerHasReservation - Er is een fout opgetreden", ex);
        }
    }
}