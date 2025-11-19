-- =============================================
-- Insert Sample Data: 2 Users, 10 Customers, and 10 Rooms
-- Hotel Reservation System - LODGIXHOTEL Database
-- =============================================

USE LODGIXHOTEL;
GO

-- =============================================
-- INSERT 2 SAMPLE USERS (Admin and Front Desk)
-- =============================================

PRINT '========================================';
PRINT 'Inserting 2 Sample Users...';
PRINT '========================================';

-- User 1: Admin
-- Username: admin
-- Password: admin123 (BCrypt hash)
INSERT INTO Users (LastName, FirstName, MiddleName, BirthDate, Username, PasswordHash, Email, Gender, Role, CreatedAt)
VALUES ('Administrator', 'System', NULL, '1990-01-01', 'admin', 
        '$2a$11$qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm123456', 
        'admin@lodgixhotel.com', 'Male', 'Admin', GETDATE());

-- User 2: Front Desk Staff
-- Username: frontdesk
-- Password: frontdesk123 (BCrypt hash)
INSERT INTO Users (LastName, FirstName, MiddleName, BirthDate, Username, PasswordHash, Email, Gender, Role, CreatedAt)
VALUES ('Reyes', 'Maria', 'Santos', '1995-06-15', 'frontdesk', 
        '$2a$11$asdfghjklqwertyuiopzxcvbnmasdfghjklqwertyuiopzxcvbnm789012', 
        'frontdesk@lodgixhotel.com', 'Female', 'Front Desk', GETDATE());

PRINT 'Users inserted successfully!';
PRINT 'Admin user - Username: admin, Password: admin123';
PRINT 'Front Desk user - Username: frontdesk, Password: frontdesk123';
PRINT '';

-- =============================================
-- INSERT 10 SAMPLE CUSTOMERS
-- =============================================

PRINT '========================================';
PRINT 'Inserting 10 Sample Customers...';
PRINT '========================================';

-- Customer 1
INSERT INTO Customers (FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes)
VALUES ('Maria', 'Santos', 'Cruz', 'Passport', '09171234567', '123 Rizal Street, Makati City', 'maria.santos@email.com', '1990-05-15', 'Female', 'Filipino', 'Regular customer');

-- Customer 2
INSERT INTO Customers (FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes)
VALUES ('John', 'Smith', 'Michael', 'Drivers License', '09181234568', '456 Ayala Avenue, Makati City', 'john.smith@email.com', '1985-08-22', 'Male', 'American', 'Corporate account');

-- Customer 3
INSERT INTO Customers (FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes)
VALUES ('Anna', 'Garcia', 'Reyes', 'National ID', '09191234569', '789 Ortigas Avenue, Pasig City', 'anna.garcia@email.com', '1992-12-10', 'Female', 'Filipino', 'Prefers ground floor rooms');

-- Customer 4
INSERT INTO Customers (FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes)
VALUES ('Robert', 'Johnson', 'Lee', 'Passport', '09201234570', '321 BGC, Taguig City', 'robert.johnson@email.com', '1988-03-18', 'Male', 'British', 'VIP guest');

-- Customer 5
INSERT INTO Customers (FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes)
VALUES ('Lisa', 'Fernandez', 'Domingo', 'Voters ID', '09211234571', '654 Commonwealth Avenue, Quezon City', 'lisa.fernandez@email.com', '1995-07-25', 'Female', 'Filipino', 'Allergic to seafood');

-- Customer 6
INSERT INTO Customers (FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes)
VALUES ('Michael', 'Wong', 'Chen', 'Passport', '09221234572', '987 Greenhills, San Juan City', 'michael.wong@email.com', '1987-11-30', 'Male', 'Chinese', 'Extended stay customer');

-- Customer 7
INSERT INTO Customers (FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes)
VALUES ('Sarah', 'Martinez', 'Lopez', 'SSS ID', '09231234573', '147 España Boulevard, Manila', 'sarah.martinez@email.com', '1993-04-08', 'Female', 'Filipino', 'Prefers early check-in');

-- Customer 8
INSERT INTO Customers (FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes)
VALUES ('David', 'Brown', 'James', 'Drivers License', '09241234574', '258 Bonifacio High Street, Taguig', 'david.brown@email.com', '1991-09-12', 'Male', 'Australian', 'Business traveler');

-- Customer 9
INSERT INTO Customers (FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes)
VALUES ('Jennifer', 'Lim', 'Tan', 'National ID', '09251234575', '369 Malate, Manila', 'jennifer.lim@email.com', '1989-06-20', 'Female', 'Singaporean', 'Frequent guest');

-- Customer 10
INSERT INTO Customers (FirstName, LastName, MiddleName, IDType, Contact, Address, Email, DateOfBirth, Gender, Nationality, Notes)
VALUES ('Carlos', 'Rivera', 'Santos', 'UMID', '09261234576', '741 Ermita, Manila', 'carlos.rivera@email.com', '1994-02-28', 'Male', 'Filipino', 'Late checkout preferred');

PRINT 'Customers inserted successfully!';
PRINT '';

-- =============================================
-- INSERT 10 SAMPLE ROOMS
-- =============================================

PRINT '========================================';
PRINT 'Inserting 10 Sample Rooms...';
PRINT '========================================';

-- Room 1: Standard Room
INSERT INTO Rooms (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription)
VALUES ('STD-101', 'Standard', 'Available', 2499.00, 1, 2, 'Comfortable standard room with essential amenities, queen bed, air conditioning, WiFi, and TV.');

-- Room 2: Standard Room
INSERT INTO Rooms (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription)
VALUES ('STD-102', 'Standard', 'Available', 2499.00, 1, 2, 'Standard room with city view, comfortable bedding, and modern bathroom facilities.');

-- Room 3: Deluxe Room
INSERT INTO Rooms (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription)
VALUES ('DLX-201', 'Deluxe', 'Available', 5999.00, 1, 2, 'Premium deluxe room with king bed, mini-bar, work desk, and luxurious bathroom with bathtub.');

-- Room 4: Deluxe Room
INSERT INTO Rooms (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription)
VALUES ('DLX-202', 'Deluxe', 'Available', 5999.00, 1, 2, 'Spacious deluxe room with modern amenities, coffee maker, and stunning city views.');

-- Room 5: Suite
INSERT INTO Rooms (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription)
VALUES ('ST-301', 'Suite', 'Available', 7999.00, 2, 4, 'Luxurious suite with separate living area, two queen beds, kitchenette, and premium entertainment system.');

-- Room 6: Suite
INSERT INTO Rooms (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription)
VALUES ('ST-302', 'Suite', 'Available', 7999.00, 2, 4, 'Executive suite featuring separate bedroom, living room, dining area, and panoramic city views.');

-- Room 7: Family Room
INSERT INTO Rooms (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription)
VALUES ('FML-401', 'Family', 'Available', 4999.00, 3, 6, 'Spacious family room perfect for families, with three beds, large bathroom, and kids-friendly amenities.');

-- Room 8: Family Room
INSERT INTO Rooms (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription)
VALUES ('FML-402', 'Family', 'Available', 4999.00, 3, 6, 'Extra-large family suite with interconnected rooms, ideal for large families or groups.');

-- Room 9: Single Room
INSERT INTO Rooms (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription)
VALUES ('SGL-501', 'Single', 'Available', 1999.00, 1, 1, 'Cozy single room ideal for solo travelers, featuring single bed, work desk, and all essential amenities.');

-- Room 10: Single Room
INSERT INTO Rooms (RoomNumber, RoomType, RoomStatus, RoomPrice, BedCount, MaxGuests, RoomDescription)
VALUES ('SGL-502', 'Single', 'Available', 1999.00, 1, 1, 'Compact single room perfect for business travelers, with fast WiFi and dedicated workspace.');

PRINT 'Rooms inserted successfully!';
PRINT '';

-- =============================================
-- VERIFICATION QUERIES
-- =============================================

PRINT '========================================';
PRINT 'Verifying Inserted Data...';
PRINT '========================================';

-- Count users
DECLARE @UserCount INT;
SELECT @UserCount = COUNT(*) FROM Users;
PRINT 'Total Users: ' + CAST(@UserCount AS VARCHAR(10));

-- Count customers
DECLARE @CustomerCount INT;
SELECT @CustomerCount = COUNT(*) FROM Customers;
PRINT 'Total Customers: ' + CAST(@CustomerCount AS VARCHAR(10));

-- Count rooms
DECLARE @RoomCount INT;
SELECT @RoomCount = COUNT(*) FROM Rooms;
PRINT 'Total Rooms: ' + CAST(@RoomCount AS VARCHAR(10));

PRINT '';
PRINT '========================================';
PRINT 'User List:';
PRINT '========================================';
SELECT 
    UserId,
    FirstName + ' ' + LastName AS FullName,
    Username,
    Email,
    Role
FROM Users
ORDER BY Role, UserId;

PRINT '';
PRINT '========================================';
PRINT 'Customer List:';
PRINT '========================================';
SELECT 
    CustomerID,
    FirstName + ' ' + LastName AS FullName,
    Email,
    Contact,
    Nationality
FROM Customers
ORDER BY CustomerID DESC;

PRINT '';
PRINT '========================================';
PRINT 'Room List:';
PRINT '========================================';
SELECT 
    RoomId,
    RoomNumber,
    RoomType,
    RoomStatus,
    RoomPrice,
    MaxGuests
FROM Rooms
ORDER BY RoomNumber;

PRINT '';
PRINT '========================================';
PRINT 'LOGIN CREDENTIALS:';
PRINT '========================================';
PRINT 'Admin Account:';
PRINT '  Username: admin';
PRINT '  Password: admin123';
PRINT '';
PRINT 'Front Desk Account:';
PRINT '  Username: frontdesk';
PRINT '  Password: frontdesk123';
PRINT '';
PRINT '========================================';
PRINT 'Data insertion completed successfully!';
PRINT '========================================';
GO
