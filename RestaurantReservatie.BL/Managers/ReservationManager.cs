﻿using RestaurantReservatie.BL.Exceptions;
using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Models;

namespace RestaurantReservatie.BL.Managers;

public class ReservationManager {
    IReservationRepository _reservationRepository;

    public ReservationManager(IReservationRepository reservationRepository) {
        _reservationRepository = reservationRepository;
    }

    public Reservation AddReservation(Reservation reservation) {
        try {
            if (reservation == null) throw new ReservationManagerException("Reservation Cannot be null.");
            if (_reservationRepository.ReservationExists(reservation.Id))
                throw new ReservationManagerException("Reservation exists already.");
            return _reservationRepository.AddReservation(reservation);
        }
        catch (Exception) {
            throw new ReservationManagerException("AddReservation - Er is een fout opgetreden");
        }
    }

    public List<Reservation> GetReservations() {
        return _reservationRepository.GetReservations();
    }

    public void DeleteReservation(int id) {
        try {
            if (id == null) throw new ReservationManagerException("Reservatie mag niet null zijn.");
            if (!_reservationRepository.ReservationExists(id))
                throw new ReservationManagerException("Reservatie bestaat niet.");
           
            _reservationRepository.DeleteReservation(id);
        }
        catch (Exception ex) {
            throw new ReservationManagerException("VerwijderReservatie - Er is een fout opgetreden", ex);
        }
    }

    public List<Reservation> GetPersonalReservations(int customerId) {
        if (customerId <= 0) throw new ReservationManagerException("CustomerId cannot be 0 or lower.");

        return _reservationRepository.GetPersonalReservations(customerId);
    }

    public Reservation UpdateReservation(Reservation reservation) {
        try {
            if (reservation == null) throw new ReservationManagerException("Reservatie mag niet null zijn.");
            if (!_reservationRepository.ReservationExists(reservation.Id))
                throw new ReservationManagerException("Reservatie bestaat niet.");
            if (reservation.Date < DateTime.Now)
                throw new ReservationManagerException("Reservatie is al geweest.");
            if (_reservationRepository.GetReservations()
                .Any(r => r.Id != reservation.Id && r.Date == reservation.Date)) {
                throw new ReservationManagerException("Er bestaat al een reservatie met dezelfde datum.");
            }

            return _reservationRepository.UpdateReservation(reservation);
        }
        catch (Exception ex) {
            throw new ReservationManagerException("UpdateReservatie - Er is een fout opgetreden", ex);
        }
    }
    
    public bool ReservationExists(int id) {
        return _reservationRepository.ReservationExists(id);
    }
}