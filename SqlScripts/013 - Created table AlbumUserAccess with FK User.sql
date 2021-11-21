IF NOT EXISTS (SELECT * FROM sysobjects WHERE [Name]='AlbumUserAccess' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[AlbumUserAccess](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[UserId] [int] NOT NULL,
		[IsActive] [bit] NOT NULL,
	 CONSTRAINT [PK_AlbumUserAccess] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[AlbumUserAccess]  WITH CHECK ADD  CONSTRAINT [FK_AlbumUserAccess_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])
	ALTER TABLE [dbo].[AlbumUserAccess]  CHECK CONSTRAINT [FK_AlbumUserAccess_User]


END