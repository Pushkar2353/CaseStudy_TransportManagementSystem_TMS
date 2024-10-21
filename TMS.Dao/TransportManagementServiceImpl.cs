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
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
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
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Updates information about an existing vehicle
        public bool UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    conn.Open();
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
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Deletes a vehicle based on its ID
        public bool DeleteVehicle(int vehicleId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Vehicle WHERE VehicleID = @VehicleID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Schedules a trip for a vehicle on a specified route
        public bool ScheduleTrip(int vehicleId, int routeId, string departureDate, string arrivalDate)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    conn.Open();
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
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Cancels a scheduled trip
        public bool CancelTrip(int tripId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Trip SET Status = 'Cancelled' WHERE TripID = @TripID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TripID", tripId);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Books a trip for a passenger
        public bool BookTrip(int tripId, int passengerId, string bookingDate)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    conn.Open();
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
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Cancels a booking
        public bool CancelBooking(int bookingId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    conn.Open();

                    string query = "UPDATE Booking SET Status = 'Cancelled' WHERE BookingID = @BookingID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BookingID", bookingId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Allocates a driver to a trip
        public bool AllocateDriver(int tripId, int driverId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    conn.Open();

                    string query = "INSERT INTO DriverAllocation (TripID, DriverID, AllocationDate) VALUES (@TripID, @DriverID, @AllocationDate)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TripID", tripId);
                    cmd.Parameters.AddWithValue("@DriverID", driverId);
                    cmd.Parameters.AddWithValue("@AllocationDate", DateTime.Now);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Deallocates a driver from a trip
        public bool DeallocateDriver(int tripId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM DriverAllocation WHERE TripID = @TripID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TripID", tripId);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Retrieves bookings made by a specific passenger
        public List<Booking> GetBookingsByPassenger(int passengerId)
        {
            List<Booking> bookings = new List<Booking>();

                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    conn.Open();
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
                conn.Open();
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
                conn.Open();
                string query = "SELECT * FROM Driver WHERE DriverID NOT IN (SELECT DriverID FROM DriverAllocation)";
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
                conn.Open();
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
                                VehicleID = Convert.ToInt32(reader["VehicleID"]),
                                Model = reader["Model"].ToString(),
                                Capacity = Convert.ToInt32(reader["Capacity"]),
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
                conn.Open();

                string query = "SELECT * FROM Booking WHERE BookingID = @BookingID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingId);

                SqlDataReader reader = cmd.ExecuteReader();
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
        // Method to get all vehicles
        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                string query = "SELECT VehicleID, Model, Capacity, Type, Status FROM Vehicle";
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Vehicle vehicle = new Vehicle
                        {
                            VehicleID = reader.GetInt32(0),
                            Model = reader.GetString(1),
                            Capacity = reader.GetDecimal(2),
                            Type = reader.GetString(3),
                            Status = reader.GetString(4)
                        };
                        vehicles.Add(vehicle);
                    }
                }
            }
            return vehicles;
        }

        // Method to get all trips
        public List<Trip> GetAllTrips()
        {
            List<Trip> trips = new List<Trip>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                string query = "SELECT TripID, VehicleID, RouteID, DepartureDate, ArrivalDate FROM Trip";
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Trip trip = new Trip
                        {
                            TripID = reader.GetInt32(0),
                            VehicleID = reader.GetInt32(1),
                            RouteID = reader.GetInt32(2),
                            DepartureDate = reader.GetDateTime(3),
                            ArrivalDate = reader.GetDateTime(4)
                        };
                        trips.Add(trip);
                    }
                }
            }
            return trips;
        }

        // Method to get all routes
        public List<Route> GetAllRoutes()
        {
            List<Route> routes = new List<Route>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                string query = "SELECT RouteID, StartDestination, EndDestination, Distance FROM Route";
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Route route = new Route
                        {
                            RouteID = reader.GetInt32(0),
                            StartDestination = reader.GetString(1),
                            EndDestination = reader.GetString(2),
                            Distance = reader.GetDecimal(3)
                        };
                        routes.Add(route);
                    }
                }
            }
            return routes;
        }

        // Method to get all bookings
        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                string query = "SELECT BookingID, TripID, PassengerID, BookingDate, Status FROM Booking";
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            BookingID = reader.GetInt32(0),
                            TripID = reader.GetInt32(1),
                            PassengerID = reader.GetInt32(2),
                            BookingDate = reader.GetDateTime(3),
                            Status = reader.GetString(4)
                        };
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;
        }
        public List<Passenger> GetAllPassengers()
        {
            List<Passenger> passengers = new List<Passenger>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                string query = "SELECT PassengerID, FirstName, Gender, Age, Email, PhoneNumber FROM Passenger";
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Passenger passenger = new Passenger
                        {
                            PassengerID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            Gender = reader.GetString(2),
                            Age = reader.GetInt32(3),
                            Email = reader.GetString(4),
                            PhoneNumber = reader.GetString(5)
                        };
                        passengers.Add(passenger);
                    }
                }
            }
            return passengers;
        }
    }
}



