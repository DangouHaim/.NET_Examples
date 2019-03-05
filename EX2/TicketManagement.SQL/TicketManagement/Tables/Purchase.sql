CREATE TABLE [dbo].[Purchase]
(
	[Id] int identity primary key,
	[UserId] INT NOT NULL,
	[EventName] NVARCHAR (255) NOT NULL,
	[EventDescription] NVARCHAR (255) NOT NULL,
	[EventDate] DATETIME NOT NULL,
	[AreaDescription] NVARCHAR (255) NOT NULL,
	[SeatRow] INT NOT NULL,
	[SeatNumber] INT NOT NULL,
	[Date] DATETIME NOT NULL
)
