CREATE TABLE [dbo].[UserAssetTransactions]
(
	[Id] BIGINT IDENTITY(1, 1) NOT NULL,

	[UserId]      INT NOT NULL,
	[UserAssetId] BIGINT NOT NULL,
	[AssetId]     INT NOT NULL,
	[CurrencyId]  INT NOT NULL,

	[TypeId]      INT NOT NULL,

	[Quantity]    DECIMAL(38, 18) NOT NULL,
	[Amount]      DECIMAL(38, 18) NOT NULL,
	[Price]       DECIMAL(38, 18) NOT NULL,

	[ExecutedAt] DATETIME2 NOT NULL,
	[CreatedAt]  DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[UpdatedAt] DATETIME2 NULL,
	[DeletedAt] DATETIME2 NULL,

	CONSTRAINT [PK_UserAssetTransactions] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_UserAssetTransactions_UserAssets_Id] FOREIGN KEY ([UserAssetId]) REFERENCES [dbo].[UserAssets]([Id]),
	CONSTRAINT [FK_UserAssetTransactions_Assets_Id] FOREIGN KEY ([AssetId]) REFERENCES [dbo].[Assets]([Id]),
	CONSTRAINT [FK_UserAssetTransactions_Currencies_Id] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies]([Id]),
	CONSTRAINT [FK_UserAssetTransactions_TransactionTypes_Id] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[TransactionTypes]([Id]),
	CONSTRAINT [FK_UserAssetTransactions_Users_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id])
);

GO
CREATE INDEX [IX_UserAssetTransactions_UserId_ExecutedAt] ON [dbo].[UserAssetTransactions] ([UserId], [ExecutedAt] DESC) WHERE DeletedAt IS NULL;
