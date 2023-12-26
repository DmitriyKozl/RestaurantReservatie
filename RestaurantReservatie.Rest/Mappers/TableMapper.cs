using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Models;
using RestaurantReservatie.Rest.Models.Input;
using RestaurantReservatie.Rest.Models.Output;

namespace RestaurantReservatie.Rest.Mappers; 

public class TableMapper {
    public static TableOutputDTO MapFromDomain(Table t)
    {
        try
        {
            return new TableOutputDTO(t.TableId, t.RestaurantID, t.Chairs, t.TableNumber);
        }
        catch (Exception e)
        {
            throw new MapperException("MapFromDomain", e);
        }
    }
    public static Table MapToDomain(TableInputDTO tafel, int restaurantId)
    {
        try
        {
            return new Table(tafel.NumberOfSeats,tafel.TableNumber ,restaurantId);
        }
        catch (Exception e)
        {
            throw new MapperException("MapToDomain", e);
        }
    }
}