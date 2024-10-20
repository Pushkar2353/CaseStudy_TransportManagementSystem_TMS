﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_Library.TMS.Entity;
using TMS_Library.TMS.Util;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace TMS_Library.TMS.Dao
{
    public interface TransportManagementService
    {
        bool AddVehicle(Vehicle vehicle);
        bool UpdateVehicle(Vehicle vehicle);
        bool DeleteVehicle(int vehicleId);
        bool ScheduleTrip(int vehicleId, int routeId, string departureDate, string arrivalDate);
        bool CancelTrip(int tripId);
        bool BookTrip(int tripId, int passengerId, string bookingDate);
        bool CancelBooking(int bookingId);
        bool AllocateDriver(int tripId, int driverId);
        bool DeallocateDriver(int tripId);
        List<Booking> GetBookingsByPassenger(int passengerId);
        List<Booking> GetBookingsByTrip(int tripId);
        List<Driver> GetAvailableDrivers();
    }
}
