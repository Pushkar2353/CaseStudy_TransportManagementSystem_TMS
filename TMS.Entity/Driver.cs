using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Entity
{
        public class Driver
        {
            private int driverID;
            private string firstName;
            private string licenseNumber;
            private string status;

            // Default Constructor
            public Driver() { }

            // Parameterized Constructor
            public Driver(int driverID, string firstName, string licenseNumber, string status)
            {
                this.driverID = driverID;
                this.firstName = firstName;
                this.licenseNumber = licenseNumber;
                this.status = status;
            }

            // Getters and Setters
            public int DriverID
            {
                get => driverID;
                set => driverID = value;
            }

            public string FirstName
            {
                get => firstName;
                set => firstName = value;
            }

            public string LicenseNumber
            {
                get => licenseNumber;
                set => licenseNumber = value;
            }

            public string Status
            {
                get => status;
                set => status = value;
            }
        }
    }
