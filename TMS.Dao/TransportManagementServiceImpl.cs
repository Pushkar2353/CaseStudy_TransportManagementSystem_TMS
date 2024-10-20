using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using TMS.Entity;
using TMS.Util;
using TMS.Exception;

namespace TMS.Dao
{
        public class TransportManagementServiceImpl : TransportManagementService
        {
            // Adds a new vehicle to the system
            public bool AddVehicle(Vehicle vehicle)
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "INSERT INTO Vehicle (Model, Capacity, Type, Status) VALUES (@Model, @Capacity, @Type, @Status)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                    cmd.Parameters.AddWithValue("@Capacity", vehicle.Capacity);
                    cmd.Parameters.AddWithValue("@Type", vehicle.Type);
                    cmd.Parameters.AddWithValue("@Status", vehicle.Status);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }

            // Updates information about an existing vehicle
            public bool UpdateVehicle(Vehicle vehicle)
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "UPDATE Vehicle SET Model = @Model, Capacity = @Capacity, Type = @Type, Status = @Status WHERE VehicleID = @VehicleID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);
                    cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                    cmd.Parameters.AddWithValue("@Capacity", vehicle.Capacity);
                    cmd.Parameters.AddWithValue("@Type", vehicle.Type);
                    cmd.Parameters.AddWithValue("@Status", vehicle.Status);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }

            // Deletes a vehicle based on its ID
            public bool DeleteVehicle(int vehicleId)
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "DELETE FROM Vehicle WHERE VehicleID = @VehicleID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@VehicleID", vehicleId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }

            // Schedules a trip for a vehicle on a specified route
            public bool ScheduleTrip(int vehicleId, int routeId, string departureDate, string arrivalDate)
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "INSERT INTO Trip (VehicleID, RouteID, DepartureDate, ArrivalDate, Status, TripType, MaxPassengers) " +
                                   "VALUES (@VehicleID, @RouteID, @DepartureDate, @ArrivalDate, 'Scheduled', 'Freight', 0)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                    cmd.Parameters.AddWithValue("@RouteID", routeId);
                    cmd.Parameters.AddWithValue("@DepartureDate", DateTime.Parse(departureDate));
                    cmd.Parameters.AddWithValue("@ArrivalDate", DateTime.Parse(arrivalDate));

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }

            // Cancels a scheduled trip
            public bool CancelTrip(int tripId)
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "UPDATE Trip SET Status = 'Cancelled' WHERE TripID = @TripID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TripID", tripId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }

            // Books a trip for a passenger
            public bool BookTrip(int tripId, int passengerId, string bookingDate)
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "INSERT INTO Booking (TripID, PassengerID, BookingDate, Status) " +
                                   "VALUES (@TripID, @PassengerID, @BookingDate, 'Confirmed')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TripID", tripId);
                    cmd.Parameters.AddWithValue("@PassengerID", passengerId);
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(bookingDate));

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }

            // Cancels a booking
            public bool CancelBooking(int bookingId)
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "UPDATE Booking SET Status = 'Cancelled' WHERE BookingID = @BookingID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BookingID", bookingId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }

            // Allocates a driver to a trip
            public bool AllocateDriver(int tripId, int driverId)
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "INSERT INTO DriverAllocation (TripID, DriverID) VALUES (@TripID, @DriverID)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TripID", tripId);
                    cmd.Parameters.AddWithValue("@DriverID", driverId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }

            // Deallocates a driver from a trip
            public bool DeallocateDriver(int tripId)
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "DELETE FROM DriverAllocation WHERE TripID = @TripID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TripID", tripId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }

            // Retrieves bookings made by a specific passenger
            public List<Booking> GetBookingsByPassenger(int passengerId)
            {
                List<Booking> bookings = new List<Booking>();

                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "SELECT * FROM Booking WHERE PassengerID = @PassengerID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PassengerID", passengerId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            BookingID = (int)reader["BookingID"],
                            TripID = (int)reader["TripID"],
                            PassengerID = (int)reader["PassengerID"],
                            BookingDate = (DateTime)reader["BookingDate"],
                            Status = reader["Status"].ToString()
                        };
                        bookings.Add(booking);
                    }
                }

                return bookings;
            }

            // Retrieves bookings associated with a specific trip
            public List<Booking> GetBookingsByTrip(int tripId)
            {
                List<Booking> bookings = new List<Booking>();

                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "SELECT * FROM Booking WHERE TripID = @TripID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TripID", tripId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            BookingID = (int)reader["BookingID"],
                            TripID = (int)reader["TripID"],
                            PassengerID = (int)reader["PassengerID"],
                            BookingDate = (DateTime)reader["BookingDate"],
                            Status = reader["Status"].ToString()
                        };
                        bookings.Add(booking);
                    }
                }

                return bookings;
            }

            // Retrieves a list of available drivers
            public List<Driver> GetAvailableDrivers()
            {
                List<Driver> drivers = new List<Driver>();

                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "SELECT * FROM Drive WHERE DriverID NOT IN (SELECT DriverID FROM DriverAllocation)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Driver driver = new Driver
                        {
                            DriverID = (int)reader["DriverID"],
                            FirstName = reader["FirstName"].ToString(),
                            LicenseNumber = reader["LicenseNumber"].ToString(),
                            Status = reader["Status"].ToString()
                        };
                        drivers.Add(driver);
                    }
                }

                return drivers;
            }
        // Retrieves a vehicle by its ID, throws VehicleNotFoundException if not found
        public Vehicle GetVehicleById(int vehicleId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open(); // Explicitly open the connection
                string query = "SELECT * FROM Vehicle WHERE VehicleID = @VehicleID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VehicleID", vehicleId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Vehicle
                            {
                                VehicleID = Convert.ToInt32(reader["VehicleID"]), // Ensure the ID is read correctly
                                Model = reader["Model"].ToString(),
                                Capacity = Convert.ToInt32(reader["Capacity"]), // Use Convert.ToInt32 here
                                Type = reader["Type"].ToString(),
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }

            throw new VehicleNotFoundException($"Vehicle with ID {vehicleId} not found.");
        }

        // Retrieves a booking by its ID, throws BookingNotFoundException if not found
        public Booking GetBookingById(int bookingId)
        {
            Booking booking = null;

            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open(); // Ensure the connection is opened

                string query = "SELECT * FROM Booking WHERE BookingID = @BookingID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingId);

                SqlDataReader reader = cmd.ExecuteReader(); // ExecuteReader requires the connection to be open
                if (reader.Read())
                {
                    booking = new Booking
                    {
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        TripID = Convert.ToInt32(reader["TripID"]),
                        PassengerID = Convert.ToInt32(reader["PassengerID"]),
                        BookingDate = (DateTime)reader["BookingDate"],
                        Status = reader["Status"].ToString()
                    };
                }
            }

            if (booking == null)
            {
                throw new BookingNotFoundException($"Booking with ID {bookingId} not found.");
            }

            return booking;
        }
    }
}



