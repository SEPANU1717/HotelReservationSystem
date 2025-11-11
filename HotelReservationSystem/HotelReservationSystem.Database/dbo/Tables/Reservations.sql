CREATE TABLE [dbo].[Reservations] (
    [ReservationId]     INT             IDENTITY (1, 1) NOT NULL,
    [CustomerName]      NVARCHAR (100)  NOT NULL,
    [RoomNumber]        NVARCHAR (20)   NULL,
    [CheckInDate]       DATETIME        NOT NULL,
    [CheckOutDate]      DATETIME        NOT NULL,
    [TotalAmount]       DECIMAL (10, 2) NOT NULL,
    [DownPayment]       DECIMAL (10, 2) NULL,
    [AmountPaid]        DECIMAL (10, 2) NULL,
    [IsDownPaymentPaid] BIT             DEFAULT ((0)) NULL,
    [PaymentMethod]     NVARCHAR (50)   NULL,
    [PaymentStatus]     NVARCHAR (30)   NULL,
    [ReservationStatus] NVARCHAR (30)   NULL,
    [DownPaymentDate]   DATETIME        NULL,
    [CreatedAt]         DATETIME        DEFAULT (getdate()) NULL,
    RoomType NVARCHAR(50) NULL
    PRIMARY KEY CLUSTERED ([ReservationId] ASC)
);

