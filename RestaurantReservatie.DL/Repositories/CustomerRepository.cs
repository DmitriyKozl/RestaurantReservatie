using Microsoft.EntityFrameworkCore;
using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;
using RestaurantReservatie.DL.Data;
using RestaurantReservatie.DL.Mapper;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Repositories;

public class CustomerRepository : ICustomerRepository {
    private readonly RestaurantReservatieContext _context;

    public CustomerRepository(string connectionString) {
        _context = new RestaurantReservatieContext(connectionString);
    }

    private void SaveAndClear() {
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
    }

    public Customer UpdateCustomer(Customer customer) {
        try {
            _context.Customer.Update(CustomerMapper.MapToDB(customer, _context));
            SaveAndClear();
            return CustomerMapper.MapToDomain(_context.Customer.Include(c => c.Location).OrderBy(c => c.CustomerId)
                .Last());
        }
        catch (Exception ex) {
            throw new RepositoryException("UpdateGebruiker - Er is een fout opgetreden", ex);
        }
    }

    public void DeleteCustomer(int id) {
        try {
            Customer_Data customerdata = _context.Customer.Find(id);
            customerdata.Deleted = true;
            _context.Customer.Update(customerdata);
            SaveAndClear();
        }
        catch (Exception ex) {
            throw new RepositoryException("VerwijderGebruiker - Er is een fout opgetreden", ex);
        }
    }

    public Customer GetCustomer(int id) {
        try {
            return CustomerMapper.MapToDomain(_context.Customer
                .Include(customer => customer.Location).First(customer => customer.CustomerId == id));
        }
        catch (Exception ex) {
            throw new RepositoryException("GeefGebruiker - Er is een fout opgetreden", ex);
        }
    }

    public List<Customer> GetAllCustomers() {
        try {
            return _context.Customer.Include(customer => customer.Location).Where(customer => !customer.Deleted)
                .Select(CustomerMapper.MapToDomain).ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GeefAlleGebruikers - Er is een fout opgetreden", ex);
        }
    }

    public Customer AddCustomer(Customer customer) {
        try {
            _context.Customer.Add(CustomerMapper.MapToDB(customer, _context));
            SaveAndClear();
            return CustomerMapper.MapToDomain(_context.Customer.Include(customer => customer.Location)
                .OrderBy(customer => customer.CustomerId).Last());
        }
        catch (Exception ex) {
            throw new RepositoryException("AddCustomer - Er is een fout opgetreden", ex);
        }
    }

    public bool CustomerExists(int id) {
        try {
            return _context.Customer.Any(c => c.CustomerId == id);
        }
        catch (Exception ex) {
            throw new RepositoryException("CustomerExists customerRepository - Er is een fout opgetreden");
        }
    }

    public bool CustomerHasReservation(int customerId) {
        try
        {
            return _context.Reservation.Any(r => r.CustomerData.CustomerId == customerId);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("GebruikerHeeftReservaties - Er is een fout opgetreden", ex);
        }    }
}