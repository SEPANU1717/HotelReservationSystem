CREATE TABLE [dbo].[Rooms] (
    [RoomId]          INT             IDENTITY (100, 1) NOT NULL,
    [RoomNumber]      VARCHAR (20)    NOT NULL,
    [RoomType]        VARCHAR (50)    NOT NULL,
    [RoomStatus]      VARCHAR (30)    DEFAULT ('Available') NOT NULL,
    [RoomPrice]       DECIMAL (10, 2) NOT NULL,
    [BedCount]        INT             DEFAULT ((1)) NOT NULL,
    [MaxGuests]       INT             DEFAULT ((2)) NOT NULL,
    [RoomDescription] TEXT            NULL,
    PRIMARY KEY CLUSTERED ([RoomId] ASC),
    UNIQUE NONCLUSTERED ([RoomNumber] ASC)
);

