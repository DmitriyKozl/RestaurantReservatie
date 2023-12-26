using RestaurantReservatie.BL.Models;
using RestaurantReservatie.Rest.Models.Input;
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

    public static Customer MapToDomain(CustomerInputDTO customerInputDto) {
        return new Customer(
            customerInputDto.Name,
            customerInputDto.PhoneNumber,
            new Location(
                customerInputDto.PostalCode, 
                customerInputDto.City,
                customerInputDto.Street,
                customerInputDto.HouseNumber),
            customerInputDto.Email);
    }
}