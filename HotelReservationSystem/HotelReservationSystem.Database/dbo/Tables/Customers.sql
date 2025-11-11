CREATE TABLE [dbo].[Customers] (
    [CustomerID]  INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50)  NOT NULL,
    [LastName]    NVARCHAR (50)  NOT NULL,
    [MiddleName]  NVARCHAR (50)  NULL,
    [IDType]      NVARCHAR (50)  NOT NULL,
    [Contact]     NVARCHAR (20)  NOT NULL,
    [Address]     NVARCHAR (200) NOT NULL,
    [Email]       NVARCHAR (100) NOT NULL,
    [DateOfBirth] DATE           NULL,
    [Gender]      NVARCHAR (20)  NULL,
    [Nationality] NVARCHAR (50)  NULL,
    [Notes]       NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);

