using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Models;
using RestaurantReservatie.DL.Data;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Mapper;

public class TableMapper {
    public static Table MapToDomain(Table_Data table) {
        try {
            return new Table(table.ID, table.Capacity, table.TableNumber, table.RestaurantID);
        }
        catch (Exception ex) {
            throw new MapperException("MapToDomain", ex);
        }
    }

    public static Table_Data MapToDB(Table tafel, RestaurantReservatieContext context) {
        try {
            Table_Data t = context.Table.Find(tafel.TableId);
            if (t != null) {
                t.Capacity = tafel.Chairs;
                t.TableNumber = tafel.TableNumber;
                t.RestaurantID = tafel.RestaurantID;
                return t;
            }

            return new Table_Data(tafel.Chairs, tafel.TableNumber, tafel.RestaurantID);
        }
        catch (Exception ex) {
            throw new MapperException("MapToDB", ex);
        }
    }
}