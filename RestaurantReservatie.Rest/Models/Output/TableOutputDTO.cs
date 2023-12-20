namespace RestaurantReservatie.Rest.Models.Output;

public class TableOutputDTO {
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int TableNumber { get; set; }
    public int NumberOfSeats { get; set; }

    public TableOutputDTO(int id, int restaurantId, int numberOfSeats, int tableNumber) {
        Id = id;
        RestaurantId = restaurantId;
        NumberOfSeats = numberOfSeats;
        TableNumber = tableNumber;
    }
}