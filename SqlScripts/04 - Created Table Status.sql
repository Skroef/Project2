IF NOT EXISTS (SELECT * FROM sysobjects WHERE [Name]='Status' and xtype='U')
BEGIN

CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(64) NOT NULL
 CONSTRAINT [PK_StatusId] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)

Insert Into Status Values('Active')
Insert Into Status Values('Do Not Disturb')
Insert Into Status Values('Away')
Insert Into Status Values('Offline')

END