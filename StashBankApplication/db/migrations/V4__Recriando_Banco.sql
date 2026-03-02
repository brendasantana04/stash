-- Creates domain tables found in the Model folder.
-- Review before running and backup your database.
SET XACT_ABORT ON;
BEGIN TRANSACTION;

-- Users
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'user')
BEGIN
    CREATE TABLE [user] (
        [id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [name] VARCHAR(100) NOT NULL,
        [email] VARCHAR(256) NULL,
        [created_on] DATETIME2 NOT NULL DEFAULT (SYSUTCDATETIME()),
        [active] BIT NOT NULL DEFAULT(1)
    );
END;

-- Accounts
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'account')
BEGIN
    CREATE TABLE [account] (
        [id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [user_id] BIGINT NOT NULL,
        [name] VARCHAR(40) NOT NULL,
        [funds] DECIMAL(18,2) NOT NULL DEFAULT(0.00),
        [created_on] DATETIME2 NOT NULL DEFAULT (SYSUTCDATETIME()),
        [active] BIT NOT NULL DEFAULT(1),
        CONSTRAINT FK_account_user FOREIGN KEY([user_id]) REFERENCES [user]([id])
    );

    CREATE INDEX IX_account_user_id ON [account]([user_id]);
END;

-- Cards
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'card')
BEGIN
    CREATE TABLE [card] (
        [id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [accountId] BIGINT NOT NULL,
        [tier] VARCHAR(16) NOT NULL,
        [limit] DECIMAL(18,2) NOT NULL DEFAULT(0.00),
        [available_credit] DECIMAL(18,2) NOT NULL DEFAULT(0.00),
        [issued_at] DATETIME2 NULL,
        [is_active] BIT NOT NULL DEFAULT(1),
        [created_on] DATETIME2 NOT NULL DEFAULT (SYSUTCDATETIME()),
        CONSTRAINT FK_card_account FOREIGN KEY([accountId]) REFERENCES [account]([id])
    );

    CREATE INDEX IX_card_accountId ON [card]([accountId]);
END;

-- Transactions
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'transaction')
BEGIN
    CREATE TABLE [transaction] (
        [id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [accountid] BIGINT NOT NULL,
        [value] DECIMAL(18,2) NOT NULL,
        [type] VARCHAR(64) NOT NULL,
        [createdon] DATETIME2 NOT NULL DEFAULT (SYSUTCDATETIME()),
        [description] VARCHAR(512) NULL,
        CONSTRAINT FK_transaction_account FOREIGN KEY([accountid]) REFERENCES [account]([id])
    );

    CREATE INDEX IX_transaction_accountid ON [transaction]([accountid]);
END;

-- Transfers
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'transfer')
BEGIN
    CREATE TABLE [transfer] (
        [id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [from_accountid] BIGINT NOT NULL,
        [to_accountid] BIGINT NOT NULL,
        [value] DECIMAL(18,2) NOT NULL,
        [createdon] DATETIME2 NOT NULL DEFAULT (SYSUTCDATETIME()),
        [description] VARCHAR(512) NULL,
        CONSTRAINT FK_transfer_from_account FOREIGN KEY([from_accountid]) REFERENCES [account]([id]),
        CONSTRAINT FK_transfer_to_account FOREIGN KEY([to_accountid]) REFERENCES [account]([id])
    );

    CREATE INDEX IX_transfer_from_accountid ON [transfer]([from_accountid]);
    CREATE INDEX IX_transfer_to_accountid ON [transfer]([to_accountid]);
END;

COMMIT;
GO