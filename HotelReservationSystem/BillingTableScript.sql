-- Create Billing table for Hotel Reservation System
CREATE TABLE Billing (
    BillId INT IDENTITY(1,1) PRIMARY KEY,
    ReservationId INT NOT NULL,
    CustomerName NVARCHAR(100) NOT NULL,
    RoomNumber NVARCHAR(10) NOT NULL,
    RoomType NVARCHAR(50),
    CheckInDate DATETIME NOT NULL,
    CheckOutDate DATETIME NOT NULL,
    NumberOfNights INT NOT NULL,
    RoomRate DECIMAL(10,2) NOT NULL,
    RoomTotal DECIMAL(10,2) NOT NULL,
    ServiceCharges DECIMAL(10,2) DEFAULT 0,
    TaxAmount DECIMAL(10,2) DEFAULT 0,
    DiscountAmount DECIMAL(10,2) DEFAULT 0,
    AdditionalCharges DECIMAL(10,2) DEFAULT 0,
    AdditionalChargesDescription NVARCHAR(500),
    Subtotal DECIMAL(10,2) NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    AmountPaid DECIMAL(10,2) DEFAULT 0,
    BalanceDue DECIMAL(10,2) NOT NULL,
    PaymentMethod NVARCHAR(50),
    PaymentStatus NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    BillDate DATETIME NOT NULL DEFAULT GETDATE(),
    DueDate DATETIME NOT NULL,
    Notes NVARCHAR(1000),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- Add foreign key constraint (optional - depends on your Reservations table structure)
-- ALTER TABLE Billing ADD CONSTRAINT FK_Billing_Reservation 
-- FOREIGN KEY (ReservationId) REFERENCES Reservations(ReservationId);

-- Create indexes for better performance
CREATE INDEX IX_Billing_ReservationId ON Billing(ReservationId);
CREATE INDEX IX_Billing_CustomerName ON Billing(CustomerName);
CREATE INDEX IX_Billing_PaymentStatus ON Billing(PaymentStatus);
CREATE INDEX IX_Billing_BillDate ON Billing(BillDate);
CREATE INDEX IX_Billing_DueDate ON Billing(DueDate);

-- Insert sample data for testing
INSERT INTO Billing (
    ReservationId, CustomerName, RoomNumber, RoomType, CheckInDate, CheckOutDate, 
    NumberOfNights, RoomRate, RoomTotal, ServiceCharges, TaxAmount, DiscountAmount, 
    AdditionalCharges, AdditionalChargesDescription, Subtotal, TotalAmount, 
    AmountPaid, BalanceDue, PaymentMethod, PaymentStatus, BillDate, DueDate, Notes
) VALUES 
(1, 'John Smith', '101', 'Standard', '2024-01-15', '2024-01-18', 3, 2499.00, 7497.00, 
 500.00, 799.70, 0.00, 200.00, 'Minibar charges', 8197.00, 8996.70, 0.00, 8996.70, 
 NULL, 'Pending', GETDATE(), DATEADD(DAY, 30, GETDATE()), 'New customer booking'),

(2, 'Jane Doe', '205', 'Deluxe', '2024-01-20', '2024-01-22', 2, 5999.00, 11998.00, 
 800.00, 1279.80, 500.00, 0.00, NULL, 12298.00, 13577.80, 13577.80, 0.00, 
 'Credit Card', 'Paid', GETDATE(), DATEADD(DAY, 30, GETDATE()), 'VIP customer - paid in full'),

(3, 'Bob Johnson', '301', 'Suite', '2024-01-25', '2024-01-28', 3, 7999.00, 23997.00, 
 1200.00, 2519.70, 1000.00, 500.00, 'Spa services', 24697.00, 27216.70, 15000.00, 12216.70, 
 'Bank Transfer', 'Partially Paid', GETDATE(), DATEADD(DAY, 15, GETDATE()), 'Partial payment received');

-- Grant permissions (adjust as needed for your security model)
-- GRANT SELECT, INSERT, UPDATE, DELETE ON Billing TO [YourApplicationUser];

PRINT 'Billing table created successfully with sample data!';