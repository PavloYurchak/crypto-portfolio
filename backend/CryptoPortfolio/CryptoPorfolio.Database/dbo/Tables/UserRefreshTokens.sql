CREATE TABLE [dbo].[UserRefreshTokens]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] INT NOT NULL,
    [Token] NVARCHAR(500) NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [ExpiresAt] DATETIME2 NOT NULL,
    [RevokedAt] DATETIME2 NULL,
    [ReasonRevoked] NVARCHAR(250) NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,

    CONSTRAINT [PK_UserRefreshTokens] PRIMARY KEY CLUSTERED ([Id]),

    CONSTRAINT [FK_UserRefreshTokens_Users_UserId] 
        FOREIGN KEY ([UserId]) 
        REFERENCES [dbo].[Users]([Id])
        ON DELETE CASCADE
);

GO

CREATE INDEX [IX_UserRefreshTokens_UserId] 
    ON [dbo].[UserRefreshTokens] ([UserId]);

GO
CREATE INDEX [IX_UserRefreshTokens_Token] 
    ON [dbo].[UserRefreshTokens] ([Token]);