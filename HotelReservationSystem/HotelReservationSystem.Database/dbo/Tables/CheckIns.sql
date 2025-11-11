-- Minimal CheckIns table
CREATE TABLE [dbo].[CheckIns](
    [CheckInId] [int] IDENTITY(1,1) PRIMARY KEY,
    [ReservationId] [int] NOT NULL,
    [CustomerName] [varchar](100) NOT NULL,
    [RoomType] [varchar](50) NULL,
    [RoomNumber] [varchar](20) NULL,
    [CheckInDate] [datetime] NOT NULL,
    [CheckOutDate] [datetime] NOT NULL,
    [TimeArrival] [datetime] NULL,
    [TotalPrice] [decimal](18, 2) NOT NULL DEFAULT(0),
    [DownPayment] [decimal](18, 2) NOT NULL DEFAULT(0),
    [AmountPaid] [decimal](18, 2) NOT NULL DEFAULT(0),
    [CompanionCount] [int] NOT NULL DEFAULT(0),
    [PaymentMethod] [varchar](50) NULL,
    [PaymentReference] [varchar](100) NULL,
    [PaymentStatus] [varchar](20) DEFAULT('Pending'),
    [ReservationStatus] [varchar](20) DEFAULT('Pending'),
    [IsCheckedIn] [bit] NOT NULL DEFAULT(0),
    [IsCheckedOut] [bit] NOT NULL DEFAULT(0),
    [ActualCheckIn] [datetime] NULL,
    [CreatedAt] [datetime] NOT NULL DEFAULT(GETDATE())
)
GO