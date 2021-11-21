IF NOT EXISTS (SELECT * FROM sysobjects WHERE [Name]='PostLog' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[PostLog](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[UserId] [int] NOT NULL,
		[ImageUrl] varchar(MAX) NOT NULL,
		[Caption] varchar(225) NOT NULL,
		[IsActive] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
	 CONSTRAINT [PK_PostLog] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[PostLog]  WITH CHECK ADD  CONSTRAINT [FK_PostLog_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])
	ALTER TABLE [dbo].[PostLog]  CHECK CONSTRAINT [FK_PostLog_User]

END