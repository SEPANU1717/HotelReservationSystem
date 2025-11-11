CREATE TABLE [dbo].[ConfirmOrder] (
    [ServiceId]    INT             IDENTITY (100, 1) NOT NULL,
    [ServiceType]  NVARCHAR (50)   NOT NULL,
    [CustomerName] NVARCHAR (50)   NOT NULL,
    [RoomNumber]   NVARCHAR (50)   NOT NULL,
    [TotalItems]   INT             NOT NULL,
    [OrderStatus]  NVARCHAR (50)   NOT NULL,
    [ServiceFee]   DECIMAL (16, 2) NOT NULL,
    [TotalAmount]  DECIMAL (16, 2) NOT NULL,
    [GrandTotal]   DECIMAL (16, 2) NOT NULL,
    [DateCreated]  DATETIME        DEFAULT (getdate()) NULL
);

