CREATE TABLE [dbo].[Event]
(
	[Id] int primary key identity,
	[Name] nvarchar(120) NOT NULL UNIQUE,
	[Description] nvarchar(max) NOT NULL,
	[LayoutId] int NOT NULL,
	[EventDate] datetime NOT NULL
)
