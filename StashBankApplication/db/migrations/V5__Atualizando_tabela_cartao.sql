BEGIN TRANSACTION;

-- Add account_id (nullable initially to avoid blocking existing rows)
IF COL_LENGTH('dbo.card', 'account_id') IS NULL
BEGIN
    ALTER TABLE dbo.card
        ADD account_id BIGINT NULL;
END

-- Add credit_limit with a safe DEFAULT so existing rows get a valid value
IF COL_LENGTH('dbo.card', 'credit_limit') IS NULL
BEGIN
    ALTER TABLE dbo.card
        ADD credit_limit DECIMAL(18,2) NOT NULL CONSTRAINT DF_card_credit_limit DEFAULT (0.00);
END

-- Add last_upgrade_at as nullable datetime2
IF COL_LENGTH('dbo.card', 'last_upgrade_at') IS NULL
BEGIN
    ALTER TABLE dbo.card
        ADD last_upgrade_at DATETIME2 NULL;
END

-- Create index on account_id if missing
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_card_account_id' AND object_id = OBJECT_ID('dbo.card')
)
BEGIN
    CREATE INDEX IX_card_account_id ON dbo.card(account_id);
END

-- Add FK to account(id) if missing
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys
    WHERE name = 'FK_card_account_account_id'
)
BEGIN
    ALTER TABLE dbo.card
        ADD CONSTRAINT FK_card_account_account_id
        FOREIGN KEY (account_id) REFERENCES dbo.account(id) ON DELETE CASCADE;
END

COMMIT TRANSACTION;