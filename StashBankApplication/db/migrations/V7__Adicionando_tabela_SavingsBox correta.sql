CREATE TABLE SavingsBox (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Balance DECIMAL(18,2) NOT NULL,
    AccountId BIGINT NOT NULL,

    CONSTRAINT FK_SavingsBox_Account
        FOREIGN KEY (AccountId)
        REFERENCES Account(Id)
);