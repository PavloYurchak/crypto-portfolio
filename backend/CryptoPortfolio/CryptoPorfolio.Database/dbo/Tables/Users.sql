CREATE TABLE [dbo].[Users]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,

    [Email]         NVARCHAR(256) NOT NULL,
    [UserName]      NVARCHAR(100) NOT NULL,
    [UserType]      NVARCHAR(50)  NOT NULL DEFAULT 'User',

    [PasswordHash]  NVARCHAR(512) NOT NULL,
    [PasswordSalt]  NVARCHAR(512) NULL,
    [PasswordAlgo]  NVARCHAR(50)  NULL,

    [EmailConfirmed]   BIT NOT NULL DEFAULT 0,

    [IsLockedOut]      BIT NOT NULL DEFAULT 0,
    [LockoutEndAt]     DATETIME2 NULL,
    [FailedAccessCount] INT NOT NULL DEFAULT 0,

    [TwoFactorEnabled]          BIT NOT NULL DEFAULT 0,
    [TwoFactorSecretKey]        NVARCHAR(256) NULL,
    [TwoFactorBackupCodes]      NVARCHAR(MAX) NULL,
    [TwoFactorLastUpdatedAt]    DATETIME2 NULL,

    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedAt] DATETIME2 NULL,
    [DeletedAt] DATETIME2 NULL,

    [IsActive] BIT NOT NULL DEFAULT 1,

    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id])
);

GO
CREATE UNIQUE INDEX [IXU_Users_Email] 
ON [dbo].[Users] ([Email]) 
WHERE [DeletedAt] IS NULL;

GO
CREATE UNIQUE INDEX [IXU_Users_UserName] 
ON [dbo].[Users] ([UserName]) 
WHERE [DeletedAt] IS NULL;

GO
CREATE UNIQUE INDEX [IXU_Users_UserType]
ON [dbo].[Users] ([UserType]) 
WHERE [UserType] = 'Admin' AND [DeletedAt] IS NULL;
