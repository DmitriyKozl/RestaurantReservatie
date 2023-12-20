using RestaurantReservatie.BL.Models;
using RestaurantReservatie.Rest.Models.Output;

namespace RestaurantReservatie.Rest.Mappers;

public class CustomerMapper {
    public static CustomerOutputDTO MapFromDomain(Customer customer) {
        return new CustomerOutputDTO(
            customer.CustomerId,
            customer.Name,
            customer.Email,
            customer.Number, customer.Location);
    }

    public static Customer MapToDomain(CustomerOutputDTO customerOutputDto) {
        return new Customer(
            customerOutputDto.CustomerID,
            customerOutputDto.Name, 
            customerOutputDto.PhoneNumber,
            customerOutputDto.Location, 
            customerOutputDto.Email);
    }
}