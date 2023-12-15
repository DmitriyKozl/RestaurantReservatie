using RestaurantReservatie.BL.Models;
using RestaurantReservatie.DL.Data;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Mapper; 

public class CustomerMapper {

    private readonly RestaurantReservatieContext _context;
    
    public CustomerMapper(RestaurantReservatieContext context) {
        _context = context;
    }
    
    public Customer_Data MapCustomerToData(Customer customer) {
    }
    
  
    
}