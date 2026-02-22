CREATE TABLE [dbo].[UserAssets]
(
	[Id] BIGINT IDENTITY(1, 1) NOT NULL,

	[UserId]  INT NOT NULL,
	[AssetId] INT NOT NULL,
	[CurrencyId] INT NOT NULL,

	[Quantity] DECIMAL(38, 18) NOT NULL DEFAULT 0,

	[CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[UpdatedAt] DATETIME2 NULL,
	[DeletedAt] DATETIME2 NULL,

	CONSTRAINT [PK_UserAssets] PRIMARY KEY CLUSTERED ([Id])
);

GO
ALTER TABLE [dbo].[UserAssets]
ADD CONSTRAINT [FK_UserAssets_Assets_Id]
FOREIGN KEY ([AssetId]) REFERENCES [dbo].[Assets]([Id]);

GO
ALTER TABLE [dbo].[UserAssets]
ADD CONSTRAINT [FK_UserAssets_Currencies_Id]
FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies]([Id]);

GO
CREATE UNIQUE INDEX [IXU_UserAssets_UserId_AssetId_CurrencyId] ON [dbo].[UserAssets] ([UserId], [AssetId], [CurrencyId]) WHERE [DeletedAt] IS NULL;
