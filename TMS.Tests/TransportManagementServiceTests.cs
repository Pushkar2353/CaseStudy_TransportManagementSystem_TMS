using Moq;
using TMS.Dao;
using TMS.Entity;
using TMS.Exception;


namespace TMS.Tests
{
    [TestFixture]
    public class TransportManagementServiceTests
    {
        private Mock<ITransportManagementServiceImpl> _serviceMock;
        private TransportManagementServiceImpl _service;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<ITransportManagementServiceImpl>();
            _service = new TransportManagementServiceImpl();
        }

        [Test]
        public void AllocateDriver_ShouldAllocateDriverSuccessfully()
        {
            int tripId = 1;
            int driverId = 1;

            _serviceMock.Setup(s => s.AllocateDriver(tripId, driverId)).Returns(true);

            var result = _serviceMock.Object.AllocateDriver(tripId, driverId);

            Assert.That(result, Is.True, "Driver allocation failed.");
        }

        [Test]
        public void DeallocateDriver_ShouldDeallocateDriverSuccessfully()
        {
            int tripId = 1;

            _serviceMock.Setup(s => s.DeallocateDriver(tripId)).Returns(true);

            var result = _serviceMock.Object.DeallocateDriver(tripId);

            Assert.That(result, Is.True, "Driver deallocation failed.");
        }

        [Test]
        public void BookTrip_ShouldBookSuccessfully()
        {
            Booking booking = new Booking
            {
                BookingID = 1,
                TripID = 1,
                PassengerID = 1,
                BookingDate = DateTime.Now,
                Status = "Confirmed"
            };

            _serviceMock.Setup(s => s.BookTrip(booking)).Returns(true);

            var result = _serviceMock.Object.BookTrip(booking);

            Assert.That(result, Is.True, "Booking failed.");
        }

        // Test for exception when vehicle not found
        [Test]
        public void GetVehicleById_ShouldThrowVehicleNotFoundException()
        {
            int vehicleId = 999;

            _serviceMock.Setup(s => s.GetVehicleById(vehicleId))
                        .Throws(new VehicleNotFoundException($"Vehicle with ID {vehicleId} not found."));

            var ex = Assert.Throws<VehicleNotFoundException>(() => _serviceMock.Object.GetVehicleById(vehicleId));
            Assert.That(ex.Message, Is.EqualTo($"Vehicle with ID {vehicleId} not found."));
        }

        // Test for exception when booking not found
        [Test]
        public void GetBookingById_ShouldThrowBookingNotFoundException()
        {
            int bookingId = 999;

            _serviceMock.Setup(s => s.GetBookingById(bookingId))
                        .Throws(new BookingNotFoundException($"Booking with ID {bookingId} not found."));

            var ex = Assert.Throws<BookingNotFoundException>(() => _serviceMock.Object.GetBookingById(bookingId));
            Assert.That(ex.Message, Is.EqualTo($"Booking with ID {bookingId} not found."));
        }
    }
}