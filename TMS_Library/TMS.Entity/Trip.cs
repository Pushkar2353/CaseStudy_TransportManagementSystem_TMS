using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Library.TMS.Entity
{
    public class Trip
        {
            private int tripID;
            private int vehicleID;
            private int routeID;
            private DateTime departureDate;
            private DateTime arrivalDate;
            private string status;
            private string tripType;
            private int maxPassengers;

            // Default Constructor
            public Trip() { }

            // Parameterized Constructor
            public Trip(int tripID, int vehicleID, int routeID, DateTime departureDate, DateTime arrivalDate, string status, string tripType, int maxPassengers)
            {
                this.tripID = tripID;
                this.vehicleID = vehicleID;
                this.routeID = routeID;
                this.departureDate = departureDate;
                this.arrivalDate = arrivalDate;
                this.status = status;
                this.tripType = tripType;
                this.maxPassengers = maxPassengers;
            }

            // Getters and Setters
            public int TripID
            {
                get => tripID;
                set => tripID = value;
            }

            public int VehicleID
            {
                get => vehicleID;
                set => vehicleID = value;
            }

            public int RouteID
            {
                get => routeID;
                set => routeID = value;
            }

            public DateTime DepartureDate
            {
                get => departureDate;
                set => departureDate = value;
            }

            public DateTime ArrivalDate
            {
                get => arrivalDate;
                set => arrivalDate = value;
            }

            public string Status
            {
                get => status;
                set => status = value;
            }

            public string TripType
            {
                get => tripType;
                set => tripType = value;
            }

            public int MaxPassengers
            {
                get => maxPassengers;
                set => maxPassengers = value;
            }
        }
    }
