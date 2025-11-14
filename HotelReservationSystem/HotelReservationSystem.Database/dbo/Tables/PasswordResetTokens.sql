CREATE TABLE PasswordResetTokens (
    TokenId INT PRIMARY KEY IDENTITY(1,1),
    UsernameOrEmail NVARCHAR(100) NOT NULL,
    Token NVARCHAR(6) NOT NULL,
    ExpiryDate DATETIME NOT NULL,
    IsUsed BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    INDEX IX_Token (Token, UsernameOrEmail, IsUsed)
);