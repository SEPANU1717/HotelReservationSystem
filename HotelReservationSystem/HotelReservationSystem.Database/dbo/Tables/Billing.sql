CREATE TABLE [dbo].[Billing] (
    [BillId]        INT             IDENTITY (100, 1) NOT NULL,
    [ReservationId] INT             NOT NULL,
    [CustomerName]  NVARCHAR (100)  NOT NULL,
    [RoomType]      NVARCHAR (50)   NOT NULL,
    [RoomNumber]    NVARCHAR (20)   NOT NULL,
    [TotalAmount]   DECIMAL (18, 2) NOT NULL,
    [PaymentStatus] NVARCHAR (20)   NULL,
    [DateBilled]    DATETIME        DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([BillId] ASC)
);

