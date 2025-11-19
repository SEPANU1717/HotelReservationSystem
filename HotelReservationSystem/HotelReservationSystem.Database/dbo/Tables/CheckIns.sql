-- Comprehensive CheckIns table with checkout support
CREATE TABLE [dbo].[CheckIns](
    [CheckInId]           [int] IDENTITY(1,1) PRIMARY KEY,
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
    [UpdatedAt]           [datetime] NULL
);