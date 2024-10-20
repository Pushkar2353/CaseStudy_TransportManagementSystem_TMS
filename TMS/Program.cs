using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TMS.Entity;
using TMS.Util;
using TMS.Dao;
using TMS.Exception;
using Microsoft.Data.SqlClient;


namespace TMS
{
    public class TransportManagementApp
    {
        static void Main(string[] args)
        {
            TransportManagementServiceImpl service = new TransportManagementServiceImpl();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Transport Management System ---");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Update Vehicle");
                Console.WriteLine("3. Delete Vehicle");
                Console.WriteLine("4. Schedule Trip");
                Console.WriteLine("5. Cancel Trip");
                Console.WriteLine("6. Book Trip");
                Console.WriteLine("7. Cancel Booking");
                Console.WriteLine("8. Allocate Driver");
                Console.WriteLine("9. Deallocate Driver");
                Console.WriteLine("10. Get Bookings By Passenger");
                Console.WriteLine("11. Get Bookings By Trip");
                Console.WriteLine("12. Get Available Drivers");
                Console.WriteLine("13. Exit");
                Console.Write("Choose an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddVehicle(service);
                        break;
                    case 2:
                        UpdateVehicle(service);
                        break;
                    case 3:
                        DeleteVehicle(service);
                        break;
                    case 4:
                        ScheduleTrip(service);
                        break;
                    case 5:
                        CancelTrip(service);
                        break;
                    case 6:
                        BookTrip(service);
                        break;
                    case 7:
                        CancelBooking(service);
                        break;
                    case 8:
                        AllocateDriver(service);
                        break;
                    case 9:
                        DeallocateDriver(service);
                        break;
                    case 10:
                        GetBookingsByPassenger(service);
                        break;
                    case 11:
                        GetBookingsByTrip(service);
                        break;
                    case 12:
                        GetAvailableDrivers(service);
                        break;
                    case 13:
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddVehicle(TransportManagementServiceImpl service)
        {
            DisplayExistingVehicles(service);

            Console.Write("Enter Vehicle Model: ");
            string model = Console.ReadLine();
            Console.Write("Enter Vehicle Capacity: ");
            int capacity = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Vehicle Type: ");
            string type = Console.ReadLine();
            Console.Write("Enter Vehicle Status: ");
            string status = Console.ReadLine();

            Vehicle vehicle = new Vehicle { Model = model, Capacity = capacity, Type = type, Status = status };
            bool success = service.AddVehicle(vehicle);
            Console.WriteLine(success ? "Vehicle added successfully." : "Failed to add vehicle.");
        }

        static void UpdateVehicle(TransportManagementServiceImpl service)
        {
            DisplayExistingVehicles(service);

            Console.Write("Enter Vehicle ID: ");
            int vehicleId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter New Vehicle Model: ");
            string model = Console.ReadLine();
            Console.Write("Enter New Vehicle Capacity: ");
            int capacity = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter New Vehicle Type: ");
            string type = Console.ReadLine();
            Console.Write("Enter New Vehicle Status: ");
            string status = Console.ReadLine();

            Vehicle vehicle = new Vehicle { VehicleID = vehicleId, Model = model, Capacity = capacity, Type = type, Status = status };
            bool success = service.UpdateVehicle(vehicle);
            Console.WriteLine(success ? "Vehicle updated successfully." : "Failed to update vehicle.");
        }

        static void DeleteVehicle(TransportManagementServiceImpl service)
        {
            DisplayExistingVehicles(service);

            Console.Write("Enter Vehicle ID: ");
            int vehicleId = Convert.ToInt32(Console.ReadLine());
            bool success = service.DeleteVehicle(vehicleId);
            Console.WriteLine(success ? "Vehicle deleted successfully." : "Failed to delete vehicle.");
        }

        static void ScheduleTrip(TransportManagementServiceImpl service)
        {
            DisplayExistingVehicles(service);
            DisplayExistingRoutes(service);

            Console.Write("Enter Vehicle ID: ");
            int vehicleId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Route ID: ");
            int routeId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Departure Date (yyyy-mm-dd): ");
            string departureDate = Console.ReadLine();
            Console.Write("Enter Arrival Date (yyyy-mm-dd): ");
            string arrivalDate = Console.ReadLine();

            bool success = service.ScheduleTrip(vehicleId, routeId, departureDate, arrivalDate);
            Console.WriteLine(success ? "Trip scheduled successfully." : "Failed to schedule trip.");
        }

        static void CancelTrip(TransportManagementServiceImpl service)
        {
            DisplayExistingTrips(service);  // Display existing trips

            Console.Write("Enter Trip ID: ");
            int tripId = Convert.ToInt32(Console.ReadLine());
            bool success = service.CancelTrip(tripId);
            Console.WriteLine(success ? "Trip cancelled successfully." : "Failed to cancel trip.");
        }

        static void BookTrip(TransportManagementServiceImpl service)
        {
            DisplayExistingRoutes(service); 
            DisplayExistingTrips(service);
            DisplayExistingPassengers(service);
            Console.Write("Enter Trip ID: ");
            int tripId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Passenger ID: ");
            int passengerId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Booking Date (yyyy-mm-dd): ");
            string bookingDate = Console.ReadLine();

            bool success = service.BookTrip(tripId, passengerId, bookingDate);
            Console.WriteLine(success ? "Trip booked successfully." : "Failed to book trip.");
        }


        static void CancelBooking(TransportManagementServiceImpl service)
        {
            DisplayExistingBookings(service);  // Display existing bookings

            Console.Write("Enter Booking ID: ");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            bool success = service.CancelBooking(bookingId);
            Console.WriteLine(success ? "Booking cancelled successfully." : "Failed to cancel booking.");
        }

        static void AllocateDriver(TransportManagementServiceImpl service)
        {
            DisplayExistingTrips(service);
            DisplayAvailableDrivers(service);

            Console.Write("Enter Trip ID: ");
            int tripId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Driver ID: ");
            int driverId = Convert.ToInt32(Console.ReadLine());

            bool success = service.AllocateDriver(tripId, driverId);
            Console.WriteLine(success ? "Driver allocated successfully." : "Failed to allocate driver.");
        }

        static void DeallocateDriver(TransportManagementServiceImpl service)
        {
            DisplayExistingTrips(service);

            Console.Write("Enter Trip ID: ");
            int tripId = Convert.ToInt32(Console.ReadLine());
            bool success = service.DeallocateDriver(tripId);
            Console.WriteLine(success ? "Driver deallocated successfully." : "Failed to deallocate driver.");
        }

        static void GetBookingsByPassenger(TransportManagementServiceImpl service)
        {
            Console.Write("Enter Passenger ID: ");
            int passengerId = Convert.ToInt32(Console.ReadLine());

            List<Booking> bookings = service.GetBookingsByPassenger(passengerId);
            if (bookings.Count > 0)
            {
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"Booking ID: {booking.BookingID}, Trip ID: {booking.TripID}, Booking Date: {booking.BookingDate}, Status: {booking.Status}");
                }
            }
            else
            {
                Console.WriteLine("No bookings found for the specified passenger.");
            }
        }

        static void GetBookingsByTrip(TransportManagementServiceImpl service)
        {
            Console.Write("Enter Trip ID: ");
            int tripId = Convert.ToInt32(Console.ReadLine());

            List<Booking> bookings = service.GetBookingsByTrip(tripId);
            if (bookings.Count > 0)
            {
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"Booking ID: {booking.BookingID}, Passenger ID: {booking.PassengerID}, Booking Date: {booking.BookingDate}, Status: {booking.Status}");
                }
            }
            else
            {
                Console.WriteLine("No bookings found for the specified trip.");
            }
        }

        static void GetAvailableDrivers(TransportManagementServiceImpl service)
        {
            List<Driver> drivers = service.GetAvailableDrivers();
            if (drivers.Count > 0)
            {
                foreach (var driver in drivers)
                {
                    Console.WriteLine($"Driver ID: {driver.DriverID}, Name: {driver.FirstName}, License Number: {driver.LicenseNumber}, Status: {driver.Status}");
                }
            }
            else
            {
                Console.WriteLine("No available drivers.");
            }
        }
        // Methods to display existing data
        static void DisplayExistingVehicles(TransportManagementServiceImpl service)
        {
            Console.WriteLine("\nExisting Vehicles:");
            List<Vehicle> vehicles = service.GetAllVehicles();
            if (vehicles.Count > 0)
            {
                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine($"Vehicle ID: {vehicle.VehicleID}, Model: {vehicle.Model}, Capacity: {vehicle.Capacity}, Type: {vehicle.Type}, Status: {vehicle.Status}");
                }
            }
            else
            {
                Console.WriteLine("No vehicles found.");
            }
        }

        static void DisplayExistingTrips(TransportManagementServiceImpl service)
        {
            Console.WriteLine("\nExisting Trips:");
            List<Trip> trips = service.GetAllTrips();
            if (trips.Count > 0)
            {
                foreach (var trip in trips)
                {
                    Console.WriteLine($"Trip ID: {trip.TripID}, Vehicle ID: {trip.VehicleID}, Route ID: {trip.RouteID}, Departure: {trip.DepartureDate}, Arrival: {trip.ArrivalDate}");
                }
            }
            else
            {
                Console.WriteLine("No trips found.");
            }
        }

        static void DisplayExistingRoutes(TransportManagementServiceImpl service)
        {
            Console.WriteLine("\nExisting Routes:");
            List<Route> routes = service.GetAllRoutes();
            if (routes.Count > 0)
            {
                foreach (var route in routes)
                {
                    Console.WriteLine($"Route ID: {route.RouteID}, Start: {route.StartDestination}, End: {route.EndDestination}");
                }
            }
            else
            {
                Console.WriteLine("No routes found.");
            }
        }

        static void DisplayAvailableDrivers(TransportManagementServiceImpl service)
        {
            Console.WriteLine("\nAvailable Drivers:");
            List<Driver> drivers = service.GetAvailableDrivers();
            if (drivers.Count > 0)
            {
                foreach (var driver in drivers)
                {
                    Console.WriteLine($"Driver ID: {driver.DriverID}, Name: {driver.FirstName}, License Number: {driver.LicenseNumber}");
                }
            }
            else
            {
                Console.WriteLine("No available drivers.");
            }
        }
        static void DisplayExistingBookings(TransportManagementServiceImpl service)
        {
            Console.WriteLine("\nExisting Bookings:");
            List<Booking> bookings = service.GetAllBookings();  // Assuming you have this method
            if (bookings.Count > 0)
            {
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"Booking ID: {booking.BookingID}, Trip ID: {booking.TripID}, Passenger ID: {booking.PassengerID}, Booking Date: {booking.BookingDate}, Status: {booking.Status}");
                }
            }
            else
            {
                Console.WriteLine("No bookings found.");
            }
        }
        static void DisplayExistingPassengers(TransportManagementServiceImpl service)
        {
            Console.WriteLine("\nExisting Passengers:");
            List<Passenger> passengers = service.GetAllPassengers();  // Assuming you have this method
            if (passengers.Count > 0)
            {
                foreach (var passenger in passengers)
                {
                    Console.WriteLine($"Passenger ID: {passenger.PassengerID}, Name: {passenger.FirstName}, Gender: {passenger.Gender}, Age: {passenger.Age}, Email: {passenger.Email}, Phone: {passenger.PhoneNumber}");
                }
            }
            else
            {
                Console.WriteLine("No passengers found.");
            }
        }
    }
}
