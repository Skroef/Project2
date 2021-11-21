if not exists (select * from sysobjects where name='User' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[Surname] [varchar](64) NOT NULL,
	[Email] [varchar](64) NULL,
	[MobileNumber] [varchar](64) NULL,

CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

END