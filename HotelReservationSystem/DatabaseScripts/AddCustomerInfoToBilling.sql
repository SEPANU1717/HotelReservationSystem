-- =============================================
-- Add Customer Information Fields to Billing Table
-- =============================================

USE HotelReservationDB;
GO

-- Add customer detail columns to Billing table
ALTER TABLE Billing 
ADD 
    CustomerEmail NVARCHAR(100) NULL,
    CustomerContact NVARCHAR(50) NULL,
    CustomerAddress NVARCHAR(200) NULL;
GO

-- Verify columns were added
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Billing' 
  AND COLUMN_NAME IN ('CustomerEmail', 'CustomerContact', 'CustomerAddress');
GO

PRINT 'Customer information columns added to Billing table successfully!';
GO
