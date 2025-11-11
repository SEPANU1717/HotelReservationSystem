CREATE TABLE [dbo].[LaundryItems] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [ItemName] VARCHAR (100)   NOT NULL,
    [Quantity] INT             NOT NULL,
    [Price]    DECIMAL (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

