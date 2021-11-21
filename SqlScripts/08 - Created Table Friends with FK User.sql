IF NOT EXISTS (SELECT * FROM sysobjects WHERE [Name]='Friends' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Friends](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[UserId] [int] NOT NULL,
		[ParentId] [int] NOT NULL,
		[IsActive] [bit] NOT NULL,
		[IsWaiting] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
	 CONSTRAINT [PK_Friends] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Friends]  WITH CHECK ADD  CONSTRAINT [FK_Friends_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])
	ALTER TABLE [dbo].[Friends]  CHECK CONSTRAINT [FK_Friends_User]

END