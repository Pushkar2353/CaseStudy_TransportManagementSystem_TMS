using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Entity
{
        public class Booking
        {
            private int bookingID;
            private int tripID;
            private int passengerID;
            private DateTime bookingDate;
            private string status;

            // Default Constructor
            public Booking() { }

            // Parameterized Constructor
            public Booking(int bookingID, int tripID, int passengerID, DateTime bookingDate, string status)
            {
                this.bookingID = bookingID;
                this.tripID = tripID;
                this.passengerID = passengerID;
                this.bookingDate = bookingDate;
                this.status = status;
            }

            // Getters and Setters
            public int BookingID
            {
                get => bookingID;
                set => bookingID = value;
            }

            public int TripID
            {
                get => tripID;
                set => tripID = value;
            }

            public int PassengerID
            {
                get => passengerID;
                set => passengerID = value;
            }

            public DateTime BookingDate
            {
                get => bookingDate;
                set => bookingDate = value;
            }

            public string Status
            {
                get => status;
                set => status = value;
            }
        }
    }
