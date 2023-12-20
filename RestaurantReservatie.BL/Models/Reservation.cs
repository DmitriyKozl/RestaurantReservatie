using RestaurantReservatie.BL.Exceptions;

namespace RestaurantReservatie.BL.Models;

public class Reservation {
    public int Id { get; set; }
    public Customer Customer { get; set; }
    public Restaurant Restaurant { get; set; }
    public int NumberOfPersons { get; set; }
    public DateTime Date { get; set; }
    public DateTime Time { get; set; }
    public int TableNumber { get; set; }


    public Reservation(int id, Customer customer, Restaurant restaurant, int numberOfPersons, DateTime date,
        DateTime time, int tableNumber) {
        Id = id;
        Customer = customer;
        Restaurant = restaurant;
        NumberOfPersons = numberOfPersons;
        Date = date;
        Time = time;
        TableNumber = tableNumber;
    }

    public Reservation(Customer customer, Restaurant restaurant, int numberOfPersons, DateTime date, DateTime time,
        int tableNumber) {
        Customer = customer;
        Restaurant = restaurant;
        NumberOfPersons = numberOfPersons;
        Date = date;
        Time = time;
        TableNumber = tableNumber;
    }

    public Reservation() {
    }
    
    public void SetId(int id) {
        if (id <= 0) throw new ReservationException("SetId - Id < 0");
        Id = id;
    }

    public override string ToString() {
        return
            $"{nameof(Id)}: {Id}, {nameof(Customer)}: {Customer}, {nameof(Restaurant)}: {Restaurant}, {nameof(NumberOfPersons)}: {NumberOfPersons}, {nameof(Date)}: {Date}, {nameof(Time)}: {Time}, {nameof(TableNumber)}: {TableNumber}";
    }
}