using RestaurantReservatie.BL.Exceptions;

namespace RestaurantReservatie.BL.Models; 

public class Reservation {
      public Reservation(int id,Customer customer, Restaurant restaurant, int numberOfPersons, DateTime date, DateTime time, int tableNumber) {
        _customer = customer;
        _restaurant = restaurant;
        NumberOfPersons = numberOfPersons;
        Date = date;
        Time = time;
        TableNumber = tableNumber;
    }
    public int Id {
        get { return _id; }
        set {
            if (value <= 0) throw new ReservationException("invalid id");
            _id = value;
        }
    }
    private int _id;
    
    public Customer Customer {
        get { return _customer; }
        set {
            if (value == null) throw new ReservationException("customer null");
            _customer = value;
        }
    }
    
    private Customer _customer;
    
    public Restaurant Restaurant {
        get { return _restaurant; }
        set {
            if (value == null) throw new ReservationException("restaurant null");
            _restaurant = value;
        }
    }
    
    private Restaurant _restaurant;
    
    public int NumberOfPersons {
        get { return _numberOfPersons; }
        set {
            if (value <= 0) throw new ReservationException("invalid number of persons");
            _numberOfPersons = value;
        }
    }
    
    private int _numberOfPersons;
    
    public DateTime Date {
        get { return _date; }
        set {
            if (value == null) throw new ReservationException("invalid date");
            _date = value;
        }
    }
    
    private DateTime _date;
    
    
    public DateTime Time {
        get { return _time; }
        set {
            if (value == null) throw new ReservationException("invalid time");
            if (value.Minute != 0 && value.Minute != 30)
                throw new ReservationException("Time must be on the hour or half-hour");
            _time = value;
        }
    }
    
    private DateTime _time;
    
    public int TableNumber {
        get { return _tableNumber; }
        set {
            if (value <= 0) throw new ReservationException("invalid table number");
            _tableNumber = value;
        }
    }
    
    
    private int _tableNumber;
    
    public override string ToString() {
        return $"{nameof(Id)}: {Id}, {nameof(Customer)}: {Customer}, {nameof(Restaurant)}: {Restaurant}, {nameof(NumberOfPersons)}: {NumberOfPersons}, {nameof(Date)}: {Date}, {nameof(Time)}: {Time}, {nameof(TableNumber)}: {TableNumber}";
    }
    
    
    

}