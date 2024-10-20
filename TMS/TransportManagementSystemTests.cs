using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using System;
using TMS.Dao;
using TMS.Entity;
using TMS.Exception;


namespace TMS
{
/*
    [TestFixture]
    public class TransportManagementServiceTests
    {
        private Mock<ITransportManagementServiceImpl> _serviceMock; // Mocking interface, not the concrete class
        private TransportManagementServiceImpl _service; // Use real implementation if needed

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<ITransportManagementServiceImpl>(); // Assuming you have an interface for the service
            _service = new TransportManagementServiceImpl(); // Use the real implementation or mocked service
        }

        [Test]
        public void AllocateDriver_ShouldAllocateDriverSuccessfully()
        {
            // Arrange
            int tripId = 1;
            int driverId = 1;

            _serviceMock.Setup(s => s.AllocateDriver(tripId, driverId)).Returns(true);

            // Act
            var result = _serviceMock.Object.AllocateDriver(tripId, driverId); // Use the mocked object

            // Assert
            Assert.IsTrue(result, "Driver allocation failed.");
        }

        [Test]
        public void DeallocateDriver_ShouldDeallocateDriverSuccessfully()
        {
            // Arrange
            int tripId = 1;

            _serviceMock.Setup(s => s.DeallocateDriver(tripId)).Returns(true);

            // Act
            var result = _serviceMock.Object.DeallocateDriver(tripId); // Use the mocked object

            // Assert
            Assert.IsTrue(result, "Driver deallocation failed.");
        }

        [Test]
        public void BookTrip_ShouldBookSuccessfully()
        {
            // Arrange
            Booking booking = new Booking { BookingID = 1, TripID = 1, PassengerID = 1, BookingDate = DateTime.Now, Status = "Confirmed" };

            _serviceMock.Setup(s => s.BookTrip(booking)).Returns(true);

            // Act
            var result = _serviceMock.Object.BookTrip(booking); // Use the mocked object

            // Assert
            Assert.IsTrue(result, "Booking failed.");
        }
    }

    [Test]
    public void GetVehicleById_ShouldThrowVehicleNotFoundException()
    {
        // Arrange
        int vehicleId = 999; // Assuming this ID does not exist

        _serviceMock.Setup(s => s.GetVehicleById(vehicleId)).Throws(new VehicleNotFoundException($"Vehicle with ID {vehicleId} not found."));

        // Act & Assert
        var ex = Assert.Throws<VehicleNotFoundException>(() => _service.GetVehicleById(vehicleId));
        Assert.That(ex.Message, Is.EqualTo($"Vehicle with ID {vehicleId} not found."));
    }

    [Test]
    public void GetBookingById_ShouldThrowBookingNotFoundException()
    {
        // Arrange
        int bookingId = 999; // Assuming this ID does not exist

        _serviceMock.Setup(s => s.GetBookingById(bookingId)).Throws(new BookingNotFoundException($"Booking with ID {bookingId} not found."));

        // Act & Assert
        var ex = Assert.Throws<BookingNotFoundException>(() => _service.GetBookingById(bookingId));
        Assert.That(ex.Message, Is.EqualTo($"Booking with ID {bookingId} not found."));
    }
*/
}

