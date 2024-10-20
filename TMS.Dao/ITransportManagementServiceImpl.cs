using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Entity;

namespace TMS.Dao
{
    public interface ITransportManagementServiceImpl
    {
        bool AllocateDriver(int tripId, int driverId);
        bool DeallocateDriver(int tripId);
        bool BookTrip(Booking booking);
        Vehicle GetVehicleById(int vehicleId);
        Booking GetBookingById(int bookingId);
    }

}
