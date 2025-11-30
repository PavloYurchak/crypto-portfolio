CREATE TABLE [dbo].[Currencies]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[Symbol]     NVARCHAR(10) NOT NULL,   
    [Name]     NVARCHAR(50) NOT NULL, 
	[CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[UpdatedAt] DATETIME2 NULL,
	[DeletedAt] DATETIME2 NULL,
    [IsActive]   BIT NOT NULL DEFAULT 1,
	CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED ([Id])
);

GO
CREATE UNIQUE INDEX [IXU_Currencies_Symbol] ON [dbo].[Currencies] ([Symbol]) WHERE [DeletedAt] IS NULL;
