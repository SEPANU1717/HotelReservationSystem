-- ========================================
-- UPDATED CheckIns Table Schema
-- ========================================
-- This is the complete, corrected CheckIns table schema
-- matching the CheckInOutModel with CustomerEmail field
-- ========================================

-- Drop existing table if you want to recreate (CAUTION: This will delete all data!)
-- DROP TABLE IF EXISTS [dbo].[CheckIns];

-- Create CheckIns table with correct schema
CREATE TABLE [dbo].[CheckIns](
    -- Primary Key
    [CheckInId]           [int] IDENTITY(1,1) PRIMARY KEY,
    
    -- Reservation Info
    [ReservationId]       [int] NOT NULL,
    [CustomerName]        [varchar](100) NOT NULL,
    [RoomType]            [varchar](50) NULL,
    [RoomNumber]          [varchar](20) NULL,
    
    -- Dates
    [CheckInDate]         [datetime] NOT NULL,
    [CheckOutDate]        [datetime] NOT NULL,
    [TimeArrival]         [datetime] NULL,
    [ActualCheckIn]       [datetime] NULL,
    [ActualCheckOut]      [datetime] NULL,
    
    -- Financial
    [TotalPrice]          [decimal](18, 2) NOT NULL DEFAULT(0),
    [DownPayment]         [decimal](18, 2) NOT NULL DEFAULT(0),
    [AmountPaid]          [decimal](18, 2) NOT NULL DEFAULT(0),
    
    -- ? UPDATED: Replaced CompanionCount with CustomerEmail
    [CustomerEmail]       [varchar](100) NULL,
    
    -- Payment
    [PaymentMethod]       [varchar](50) NULL,
    [PaymentReference]    [varchar](100) NULL,
    [PaymentStatus]       [varchar](20) DEFAULT('Pending'),
    [ReservationStatus]   [varchar](20) DEFAULT('Pending'),
    
    -- Check-in/out tracking
    [IsCheckedIn]         [bit] NOT NULL DEFAULT(0),
    [IsCheckedOut]        [bit] NOT NULL DEFAULT(0),
    [CheckedInBy]         [varchar](100) NULL,
    [CheckedOutBy]        [varchar](100) NULL,
    [CheckInNotes]        [nvarchar](500) NULL,
    [CheckOutNotes]       [nvarchar](500) NULL,
    
    -- Audit
    [CreatedAt]           [datetime] NOT NULL DEFAULT(GETDATE()),
    [UpdatedAt]           [datetime] NULL,
    
    -- Indexes for performance
    CONSTRAINT [IX_CheckIns_ReservationId] UNIQUE NONCLUSTERED ([ReservationId]),
    INDEX [IX_CheckIns_CustomerName] NONCLUSTERED ([CustomerName]),
    INDEX [IX_CheckIns_RoomNumber] NONCLUSTERED ([RoomNumber]),
    INDEX [IX_CheckIns_CheckInDate] NONCLUSTERED ([CheckInDate]),
    INDEX [IX_CheckIns_Status] NONCLUSTERED ([ReservationStatus], [IsCheckedIn], [IsCheckedOut])
);
GO

-- Create trigger to auto-update UpdatedAt timestamp
CREATE OR ALTER TRIGGER [dbo].[trg_CheckIns_UpdateTimestamp]
ON [dbo].[CheckIns]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE [dbo].[CheckIns]
    SET [UpdatedAt] = GETDATE()
    FROM [dbo].[CheckIns] ci
    INNER JOIN inserted i ON ci.CheckInId = i.CheckInId;
END;
GO

PRINT '========================================='
PRINT 'CheckIns Table Created Successfully!'
PRINT '========================================='
PRINT 'Changes from original:'
PRINT '- ? REMOVED: CompanionCount (int)'
PRINT '+ ? ADDED: CustomerEmail (varchar(100))'
PRINT '========================================='
GO
