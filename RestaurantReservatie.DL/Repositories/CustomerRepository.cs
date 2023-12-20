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
            return CustomerMapper.MapToDomain(_context.Customer.Include(g => g.Location).OrderBy(g => g.CustomerId)
                .Last());
        }
        catch (Exception ex) {
            throw new RepositoryException("UpdateCustomer - Er is een fout opgetreden", ex);
        }
    }

    public void DeleteCustomer(int id) {
        try {
            Customer_Data g = _context.Customer.Find(id);
            g.Deleted = true;
            _context.Customer.Update(g);
            SaveAndClear();
        }
        catch (Exception ex) {
            throw new RepositoryException("DeleteCustomer - Er is een fout opgetreden", ex);
        }
    }

    public Customer GetCustomer(int id) {
        try {
            return CustomerMapper
                .MapToDomain(_context.Customer
                    .Include(g => g.Location).First(g => g.CustomerId == id));
        }
        catch (Exception ex) {
            throw new RepositoryException("GetCustomers - Er is een fout opgetreden", ex);
        }
    }

    public List<Customer> GetAllCustomers() {
        try {
            return _context.Customer
                .Include(g => g.Location)
                .Where(g => g.Deleted == false)
                .Select(CustomerMapper.MapToDomain)
                .ToList();
        }
        catch (Exception ex) {
            throw new RepositoryException("GetAllCustomers - Er is een fout opgetreden", ex);
        }
    }

    public Customer AddCustomer(Customer customer) {
        try {
            _context.Customer.Add(CustomerMapper.MapToDB(customer, _context));
            SaveAndClear();
            return CustomerMapper
                .MapToDomain(_context.Customer
                    .Include(g => g.Location)
                    .OrderBy(g => g.CustomerId)
                    .Last());
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
            throw new RepositoryException("CustomerExists - Er is een fout opgetreden", ex);
        }
    }
}