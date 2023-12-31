﻿using System.Globalization;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.Rest.Models.Output; 

public class ReservationOutputDTO {
    public int ReservationId { get; set; }
    public Restaurant Restaurant { get; set; }

    public Customer Customer { get; set; }

    public int Capacity { get; set; }

    public DateTime Date { get; set; }
    
    public string Time { get; set; }

    public int Tablenumber { get; set; }
    
    public ReservationOutputDTO(int reservationId, Restaurant restaurant, Customer customer, int capacity, DateTime date,string time, int tablenumber)
    {
        ReservationId = reservationId;
        Restaurant = restaurant;
        Customer = customer;
        Date = date.Date;
        Time = time;

        Capacity = capacity;
        Tablenumber = tablenumber;
    }
}