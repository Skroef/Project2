IF NOT EXISTS (SELECT * FROM sysobjects WHERE [Name]='ViewPost' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[ViewPost](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[UserId] [int] NOT NULL,
		[PostLogId] [int] NOT NULL,
		[IsActive] [bit] NOT NULL,
	 CONSTRAINT [PK_ViewPost] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[ViewPost]  WITH CHECK ADD  CONSTRAINT [FK_ViewPost_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])
	ALTER TABLE [dbo].[ViewPost]  CHECK CONSTRAINT [FK_ViewPost_User]

	ALTER TABLE [dbo].[ViewPost]  WITH CHECK ADD  CONSTRAINT [FK_ViewPost_PostLog] FOREIGN KEY([PostLogId])
	REFERENCES [dbo].[PostLog] ([Id])
	ALTER TABLE [dbo].[ViewPost]  CHECK CONSTRAINT [FK_ViewPost_PostLog]

END