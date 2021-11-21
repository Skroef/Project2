IF NOT EXISTS (SELECT * FROM sysobjects WHERE [Name]='AlbumPhotos' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[AlbumPhotos](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PostLogId] [int] NOT NULL,
		[AlbumId] [int] NOT NULL,
		[IsActive] [bit] NOT NULL,
	 CONSTRAINT [PK_AlbumPhotos] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[AlbumPhotos]  WITH CHECK ADD  CONSTRAINT [FK_AlbumPhotos_PostLog] FOREIGN KEY([PostLogId])
	REFERENCES [dbo].[PostLog] ([Id])
	ALTER TABLE [dbo].[AlbumPhotos]  CHECK CONSTRAINT [FK_AlbumPhotos_PostLog]

	ALTER TABLE [dbo].[AlbumPhotos]  WITH CHECK ADD  CONSTRAINT [FK_AlbumPhotos_Album] FOREIGN KEY([AlbumId])
	REFERENCES [dbo].[Album] ([Id])
	ALTER TABLE [dbo].[AlbumPhotos]  CHECK CONSTRAINT [FK_AlbumPhotos_Album]


END