IF NOT EXISTS (SELECT * FROM sysobjects WHERE [Name]='PostComment' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[PostComment](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[UserId] [int] NOT NULL,
		[PostLogId] [int] NOT NULL,
		[Text] varchar(max) NOT NULL,
		[IsActive] [bit] NOT NULL,
	 CONSTRAINT [PK_PostComment] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[PostComment]  WITH CHECK ADD  CONSTRAINT [FK_PostComment_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])
	ALTER TABLE [dbo].[PostComment]  CHECK CONSTRAINT [FK_PostComment_User]

	ALTER TABLE [dbo].[PostComment]  WITH CHECK ADD  CONSTRAINT [FK_PostComment_PostLog] FOREIGN KEY([PostLogId])
	REFERENCES [dbo].[PostLog] ([Id])
	ALTER TABLE [dbo].[PostComment]  CHECK CONSTRAINT [FK_PostComment_PostLog]

END