Create database TMS
go

Use TMS
go

-- 1. Vehicles Table
CREATE TABLE Vehicle (
    VehicleID INT PRIMARY KEY IDENTITY(1,1) ,
    Model VARCHAR(255),
    Capacity DECIMAL(10, 2),
    Type VARCHAR(50),
    Status VARCHAR(50)
);

-- 2. Routes Table
CREATE TABLE Route (
    RouteID INT PRIMARY KEY IDENTITY(1,1),
    StartDestination VARCHAR(255),
    EndDestination VARCHAR(255),
    Distance DECIMAL(10, 2)
);

-- 3. Trips Table
CREATE TABLE Trip (
    TripID INT PRIMARY KEY IDENTITY(1,1),
    VehicleID INT,
    RouteID INT,
    DepartureDate DATETIME,
    ArrivalDate DATETIME,
    Status VARCHAR(50),
    TripType VARCHAR(50) DEFAULT 'Freight',
    MaxPassengers INT,
    FOREIGN KEY (VehicleID) REFERENCES Vehicle(VehicleID),
    FOREIGN KEY (RouteID) REFERENCES Route(RouteID)
);

-- 4. Passengers Table
CREATE TABLE Passenger (
    PassengerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(255),
    Gender VARCHAR(255),
    Age INT,
    Email VARCHAR(255) UNIQUE,
    PhoneNumber VARCHAR(50)
);

-- 5. Bookings Table
CREATE TABLE Booking (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    TripID INT,
    PassengerID INT,
    BookingDate DATETIME,
    Status VARCHAR(50),
    FOREIGN KEY (TripID) REFERENCES Trip(TripID),
    FOREIGN KEY (PassengerID) REFERENCES Passenger(PassengerID)
);

-- 6. Drivers Table
CREATE TABLE Driver (
    DriverID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(255) NOT NULL,
    LicenseNumber VARCHAR(255) UNIQUE NOT NULL,
    Status VARCHAR(50) NOT NULL
);

-- 7. DriverAllocations Table
CREATE TABLE DriverAllocation (
    AllocationID INT PRIMARY KEY IDENTITY(1,1),
    TripID INT,
    DriverID INT,
    AllocationDate DATETIME NOT NULL,
    FOREIGN KEY (TripID) REFERENCES Trip(TripID),
    FOREIGN KEY (DriverID) REFERENCES Driver(DriverID)
);


INSERT INTO Vehicle (Model, Capacity, Type, Status)
VALUES
('Ford Transit', 12.50, 'Van', 'Available'),
('Mercedes Actros', 40.00, 'Truck', 'On Trip'),
('Toyota Hiace', 14.00, 'Bus', 'Maintenance'),
('Volvo FH', 44.00, 'Truck', 'Available'),
('Volkswagen Crafter', 10.50, 'Van', 'On Trip'),
('Iveco Daily', 20.00, 'Truck', 'Maintenance'),
('MAN TGX', 50.00, 'Truck', 'Available'),
('Scania R450', 45.00, 'Truck', 'Available'),
('Renault Master', 15.00, 'Van', 'On Trip'),
('Fiat Ducato', 13.00, 'Van', 'Available');


INSERT INTO Route (StartDestination, EndDestination, Distance)
VALUES
('New York', 'Los Angeles', 3940.50),
('Houston', 'Miami', 1978.30),
('Chicago', 'Dallas', 1510.00),
('Seattle', 'San Francisco', 1302.25),
('Boston', 'Washington DC', 725.60),
('Phoenix', 'Las Vegas', 483.50),
('Denver', 'Salt Lake City', 839.80),
('Atlanta', 'Orlando', 729.90),
('Detroit', 'Toronto', 383.70),
('Philadelphia', 'Pittsburgh', 489.35);


INSERT INTO Trip (VehicleID, RouteID, DepartureDate, ArrivalDate, Status, TripType, MaxPassengers)
VALUES
(1, 1, '2024-10-16 09:00:00', '2024-10-20 18:00:00', 'Scheduled', 'Freight', 0),
(2, 2, '2024-10-17 08:00:00', '2024-10-18 16:00:00', 'In Progress', 'Freight', 0),
(3, 3, '2024-10-18 07:30:00', '2024-10-19 14:00:00', 'Scheduled', 'Passenger', 10),
(4, 4, '2024-10-15 10:00:00', '2024-10-16 22:00:00', 'Completed', 'Freight', 0),
(5, 5, '2024-10-14 12:00:00', '2024-10-15 20:00:00', 'Cancelled', 'Passenger', 15),
(6, 6, '2024-10-16 06:00:00', '2024-10-16 22:00:00', 'In Progress', 'Freight', 0),
(7, 7, '2024-10-19 14:00:00', '2024-10-20 23:30:00', 'Scheduled', 'Passenger', 20),
(8, 8, '2024-10-17 15:00:00', '2024-10-18 08:00:00', 'Completed', 'Freight', 0),
(9, 9, '2024-10-16 09:30:00', '2024-10-16 18:30:00', 'Scheduled', 'Passenger', 5),
(10, 10, '2024-10-17 07:00:00', '2024-10-17 19:00:00', 'Completed', 'Freight', 0);


INSERT INTO Passenger (FirstName, Gender, Age, Email, PhoneNumber)
VALUES
('John', 'Male', 30, 'john.doe@example.com', '555-123-4567'),
('Jane', 'Female', 28, 'jane.smith@example.com', '555-987-6543'),
('Michael', 'Male', 45, 'michael.jordan@example.com', '555-654-3210'),
('Emily', 'Female', 35, 'emily.blunt@example.com', '555-789-1234'),
('Chris', 'Male', 22, 'chris.evans@example.com', '555-222-3333'),
('Sarah', 'Female', 40, 'sarah.connor@example.com', '555-444-5555'),
('David', 'Male', 27, 'david.tennant@example.com', '555-666-7777'),
('Sophia', 'Female', 29, 'sophia.loren@example.com', '555-888-9999'),
('James', 'Male', 50, 'james.bond@example.com', '555-007-0000'),
('Olivia', 'Female', 32, 'olivia.wilde@example.com', '555-111-2222');


INSERT INTO Booking (TripID, PassengerID, BookingDate, Status)
VALUES
(3, 1, '2024-10-10 09:00:00', 'Confirmed'),
(3, 2, '2024-10-10 09:15:00', 'Confirmed'),
(7, 3, '2024-10-11 10:30:00', 'Cancelled'),
(5, 4, '2024-10-09 11:00:00', 'Confirmed'),
(9, 5, '2024-10-12 12:30:00', 'Completed'),
(3, 6, '2024-10-10 14:45:00', 'Confirmed'),
(7, 7, '2024-10-13 13:00:00', 'Confirmed'),
(9, 8, '2024-10-14 08:00:00', 'Completed'),
(5, 9, '2024-10-10 09:20:00', 'Cancelled'),
(9, 10, '2024-10-12 16:45:00', 'Confirmed');


INSERT INTO Driver (FirstName, LicenseNumber, Status)
VALUES
('John', 'D123456789', 'Active'),
('Jane', 'D987654321', 'Active'),
('Michael', 'D111223344', 'Inactive'),
('Emily', 'D223344556', 'Active'),
('Chris', 'D334455667', 'Active'),
('Sarah', 'D445566778', 'Inactive'),
('David', 'D556677889', 'Active'),
('Sophia', 'D667788990', 'Active'),
('James', 'D778899001', 'Inactive'),
('Olivia', 'D889900112', 'Active');


INSERT INTO DriverAllocation (TripID, DriverID, AllocationDate)
VALUES
(1, 1, '2024-10-15 08:00:00'),
(2, 2, '2024-10-15 09:00:00'),
(3, 3, '2024-10-15 10:00:00'),
(4, 4, '2024-10-16 11:00:00'),
(5, 5, '2024-10-16 12:00:00'),
(6, 6, '2024-10-17 13:00:00'),
(7, 7, '2024-10-17 14:00:00'),
(8, 8, '2024-10-18 15:00:00'),
(9, 9, '2024-10-18 16:00:00'),
(10, 10, '2024-10-19 17:00:00');


SELECT * FROM Vehicle;
SELECT * FROM Route;
SELECT * FROM Trip;
SELECT * FROM Passenger;
SELECT * FROM Booking;
SELECT * FROM Driver;
SELECT * FROM DriverAllocation;

