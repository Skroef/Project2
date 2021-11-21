IF NOT EXISTS (SELECT * FROM sysobjects WHERE [Name]='Album' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Album](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[UserId] [int] NOT NULL,
		[PostLogId] [int] NOT NULL,
		[Name] varchar(255) NOT NULL,
		[IsActive] [bit] NOT NULL,
	 CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Album]  WITH CHECK ADD  CONSTRAINT [FK_Album_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])
	ALTER TABLE [dbo].[Album]  CHECK CONSTRAINT [FK_Album_User]

	ALTER TABLE [dbo].[Album]  WITH CHECK ADD  CONSTRAINT [FK_Album_PostLog] FOREIGN KEY([PostLogId])
	REFERENCES [dbo].[PostLog] ([Id])
	ALTER TABLE [dbo].[Album]  CHECK CONSTRAINT [FK_Album_PostLog]

END