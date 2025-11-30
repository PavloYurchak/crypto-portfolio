CREATE TABLE [dbo].[TransactionTypes]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,

    [Code] NVARCHAR(50) NOT NULL,         
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(255) NULL,

    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedAt] DATETIME2 NULL,
    [DeletedAt] DATETIME2 NULL,

    [IsActive] BIT NOT NULL DEFAULT 1,

    CONSTRAINT [PK_TransactionTypes] PRIMARY KEY CLUSTERED ([Id])
);

GO

CREATE UNIQUE INDEX [IXU_TransactionTypes_Code] ON [dbo].[TransactionTypes] ([Code]) WHERE [DeletedAt] IS NULL;