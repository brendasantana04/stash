SET NOCOUNT ON;
GO

-- 1) user
IF OBJECT_ID(N'dbo.[user]', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.[user] (
        id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        created_on DATE NOT NULL CONSTRAINT DF_user_created_on DEFAULT (CONVERT(date, SYSUTCDATETIME())),
        [name] VARCHAR(40) NOT NULL,
        [email] VARCHAR(30) NOT NULL,
        [password] VARCHAR(30) NOT NULL
    );

    CREATE INDEX IX_user_email ON dbo.[user]([email]);
END
GO

-- 2) account
IF OBJECT_ID(N'dbo.[account]', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.[account] (
        id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        created_on DATE NOT NULL CONSTRAINT DF_account_created_on DEFAULT (CONVERT(date, SYSUTCDATETIME())),
        user_id BIGINT NOT NULL,
        [name] VARCHAR(40) NOT NULL,
        funds DECIMAL(18,2) NOT NULL CONSTRAINT DF_account_funds DEFAULT (0.00),
        active BIT NOT NULL CONSTRAINT DF_account_active DEFAULT (1)
    );

    ALTER TABLE dbo.[account]
        ADD CONSTRAINT FK_account_user FOREIGN KEY (user_id) REFERENCES dbo.[user](id);

    CREATE INDEX IX_account_user_id ON dbo.[account](user_id);
END
GO

-- 3) card
IF OBJECT_ID(N'dbo.[card]', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.[card] (
        id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        created_on DATE NOT NULL CONSTRAINT DF_card_created_on DEFAULT (CONVERT(date, SYSUTCDATETIME())),
        accountId BIGINT NOT NULL,
        tier VARCHAR(16) NOT NULL,               -- maps to CardTier (stored as text per model Column attribute)
        [limit] DECIMAL(18,2) NOT NULL CONSTRAINT DF_card_limit DEFAULT (0.00), -- property named CreditLimit
        available_credit DECIMAL(18,2) NOT NULL CONSTRAINT DF_card_available_credit DEFAULT (0.00),
        issued_at DATETIME2 NULL,
        is_active BIT NOT NULL CONSTRAINT DF_card_is_active DEFAULT (1)
    );

    ALTER TABLE dbo.[card]
        ADD CONSTRAINT FK_card_account FOREIGN KEY (accountId) REFERENCES dbo.[account](id);

    -- Enforce 1:1 relation (each account has at most one card) if intended by model navigation
    CREATE UNIQUE INDEX UQ_card_accountId ON dbo.[card](accountId);
END
GO

-- 4) transaction
IF OBJECT_ID(N'dbo.[transaction]', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.[transaction] (
        id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        created_on DATE NOT NULL CONSTRAINT DF_transaction_created_on DEFAULT (CONVERT(date, SYSUTCDATETIME())),
        accountid BIGINT NOT NULL,
        [type] INT NOT NULL,                     -- maps to TransactionType enum
        value DECIMAL(18,2) NOT NULL,
        description VARCHAR(255) NULL
    );

    ALTER TABLE dbo.[transaction]
        ADD CONSTRAINT FK_transaction_account FOREIGN KEY (accountid) REFERENCES dbo.[account](id);

    CREATE INDEX IX_transaction_accountid ON dbo.[transaction](accountid);
    CREATE INDEX IX_transaction_type ON dbo.[transaction]([type]);
END
GO

-- 5) transfer
IF OBJECT_ID(N'dbo.[transfer]', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.[transfer] (
        id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        created_on DATE NOT NULL CONSTRAINT DF_transfer_created_on DEFAULT (CONVERT(date, SYSUTCDATETIME())),
        id_account_to BIGINT NOT NULL,
        id_account_from BIGINT NOT NULL,
        value DECIMAL(18,2) NOT NULL
    );

    ALTER TABLE dbo.[transfer]
        ADD CONSTRAINT FK_transfer_account_to FOREIGN KEY (id_account_to) REFERENCES dbo.[account](id),
            CONSTRAINT FK_transfer_account_from FOREIGN KEY (id_account_from) REFERENCES dbo.[account](id);

    CREATE INDEX IX_transfer_to ON dbo.[transfer](id_account_to);
    CREATE INDEX IX_transfer_from ON dbo.[transfer](id_account_from);
END
GO