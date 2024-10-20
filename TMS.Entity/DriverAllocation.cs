using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Entity
{
    public class DriverAllocation
    {
        private int allocationID;
        private int tripID;
        private int driverID;
        private DateTime allocationDate;

        // Default Constructor
        public DriverAllocation() { }

        // Parameterized Constructor
        public DriverAllocation(int allocationID, int tripID, int driverID, DateTime allocationDate)
        {
            this.allocationID = allocationID;
            this.tripID = tripID;
            this.driverID = driverID;
            this.allocationDate = allocationDate;
        }

        // Getters and Setters
        public int AllocationID
        {
            get => allocationID;
            set => allocationID = value;
        }

        public int TripID
        {
            get => tripID;
            set => tripID = value;
        }

        public int DriverID
        {
            get => driverID;
            set => driverID = value;
        }

        public DateTime AllocationDate
        {
            get => allocationDate;
            set => allocationDate = value;
        }
    }
}
