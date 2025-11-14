CREATE TABLE [dbo].[Companions]
(
    [CompanionId] INT IDENTITY(1,1) NOT NULL,
    [MainReservationId] INT NOT NULL,
    [CompanionName] NVARCHAR(250) NOT NULL,
    [ContactNumber] NVARCHAR(50) NULL,
    [Email] NVARCHAR(250) NULL,
    [Relationship] NVARCHAR(100) NULL,
    [RoomType] NVARCHAR(50) NOT NULL,
    [RoomNumber] VARCHAR(20) NOT NULL,  -- Changed from NVARCHAR(50) to VARCHAR(20)
    [RoomPrice] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [Nights] INT NOT NULL DEFAULT(1),
    [CheckInDate] DATETIME NOT NULL,
    [CheckOutDate] DATETIME NOT NULL,
    [TotalCost] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [CreatedAt] DATETIME NOT NULL CONSTRAINT [DF_Companions_CreatedAt] DEFAULT(GETDATE()),
    [CreatedBy] NVARCHAR(100) NULL,
    
    CONSTRAINT [PK_Companions] PRIMARY KEY CLUSTERED ([CompanionId] ASC),
    
    CONSTRAINT [FK_Companions_Reservations] FOREIGN KEY ([MainReservationId]) 
        REFERENCES [dbo].[Reservations]([ReservationId]) ON DELETE CASCADE,
    
    CONSTRAINT [FK_Companions_Rooms] FOREIGN KEY ([RoomNumber]) 
        REFERENCES [dbo].[Rooms]([RoomNumber]) ON UPDATE CASCADE
);
GO