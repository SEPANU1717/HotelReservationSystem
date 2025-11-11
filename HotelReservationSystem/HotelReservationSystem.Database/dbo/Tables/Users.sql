CREATE TABLE [dbo].[Users] (
    [UserId]       INT            IDENTITY (1, 1) NOT NULL,
    [LastName]     NVARCHAR (50)  NOT NULL,
    [FirstName]    NVARCHAR (50)  NOT NULL,
    [MiddleName]   NVARCHAR (50)  NULL,
    [BirthDate]    DATETIME       NOT NULL,
    [Username]     NVARCHAR (50)  NOT NULL,
    [PasswordHash] NVARCHAR (255) NOT NULL,
    [Email]        NVARCHAR (100) NOT NULL,
    [Gender]       NVARCHAR (10)  NOT NULL,
    [Role]         NVARCHAR (20)  NOT NULL,
    [CreatedAt]    DATETIME       DEFAULT (getdate()) NOT NULL
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);
