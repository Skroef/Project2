IF NOT EXISTS (SELECT * FROM sysobjects WHERE [Name]='UserRole' and xtype='U')
BEGIN

CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(64) NOT NULL
 CONSTRAINT [PK_UserRoleId] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)

Insert Into UserRole Values('Admin')

END