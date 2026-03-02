SET XACT_ABORT ON;
BEGIN TRANSACTION;

-- 1) Rename `AccountId` -> `accountId` if the capitalized column exists and lowercase one does not.
IF EXISTS (
    SELECT 1
    FROM sys.columns c
    JOIN sys.tables t ON c.object_id = t.object_id
    WHERE t.name = 'card' AND c.name = 'AccountId'
)
AND NOT EXISTS (
    SELECT 1
    FROM sys.columns c2
    JOIN sys.tables t2 ON c2.object_id = t2.object_id
    WHERE t2.name = 'card' AND c2.name = 'accountId'
)
BEGIN
    EXEC sp_rename N'card.AccountId', N'accountId', N'COLUMN';
END;

-- 2) Add `accountId` column if it is still missing.
IF NOT EXISTS (
    SELECT 1
    FROM sys.columns c
    JOIN sys.tables t ON c.object_id = t.object_id
    WHERE t.name = 'card' AND c.name = 'accountId'
)
BEGIN
    ALTER TABLE [card] ADD [accountId] BIGINT NULL;
END;

-- 3) Create non-unique index to speed FK lookups if it does not exist.
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'card') AND name = N'IX_card_accountId'
)
BEGIN
    CREATE INDEX IX_card_accountId ON [card]([accountId]);
END;

-- 4) Add foreign key constraint to `account(id)` if missing and `account` table exists.
IF OBJECT_ID(N'account') IS NOT NULL
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM sys.foreign_keys fk
        WHERE fk.parent_object_id = OBJECT_ID(N'card')
          AND fk.referenced_object_id = OBJECT_ID(N'account')
    )
    BEGIN
        ALTER TABLE [card]
        ADD CONSTRAINT FK_card_account
            FOREIGN KEY ([accountId]) REFERENCES [account]([id]);
    END;
END;

-- 5) If there are no NULL values, make the column NOT NULL to match the C# non-null requirement.
IF NOT EXISTS (SELECT 1 FROM [card] WHERE [accountId] IS NULL)
BEGIN
    ALTER TABLE [card] ALTER COLUMN [accountId] BIGINT NOT NULL;
END
ELSE
BEGIN
    PRINT 'Warning: [card].[accountId] contains NULLs. Populate values and re-run ALTER to set NOT NULL.';
END;

COMMIT;
GO