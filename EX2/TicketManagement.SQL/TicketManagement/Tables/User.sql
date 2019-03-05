CREATE TABLE [dbo].[User]
(
	[Id] int identity primary key,
	[Name] NVARCHAR(255) NOT NULL,
	[EMail] NVARCHAR(255) NOT NULL,
	[Password] NVARCHAR(512) NOT NULL,
	[Balance] DECIMAL NOT NULL DEFAULT 0
)
