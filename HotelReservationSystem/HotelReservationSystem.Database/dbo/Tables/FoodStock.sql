CREATE TABLE [dbo].[FoodStock] (
    [FoodId]      INT             IDENTITY (100, 1) NOT NULL,
    [FoodName]    NVARCHAR (100)  NOT NULL,
    [Description] NVARCHAR (255)  NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [Stock]       INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([FoodId] ASC)
);

