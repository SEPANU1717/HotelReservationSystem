CREATE TABLE [dbo].[OrderFood] (
    [FoodId]   INT             IDENTITY (10001, 1) NOT NULL,
    [ItemName] VARCHAR (50)    NOT NULL,
    [Quantity] INT             NOT NULL,
    [Price]    DECIMAL (16, 2) NULL,
    PRIMARY KEY CLUSTERED ([FoodId] ASC)
);

