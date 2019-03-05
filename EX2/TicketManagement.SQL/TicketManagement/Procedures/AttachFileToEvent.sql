CREATE PROCEDURE [dbo].[AttachFileToEvent]
	@EventId int,
	@Url nvarchar(1000)
AS
BEGIN
	INSERT INTO [File] ([Url]) VALUES (@Url);

	DECLARE @IdFile as INT;
	SET @IdFile = (SELECT CAST(SCOPE_IDENTITY() AS INT));

	INSERT INTO EventFile (EventId, FileId) VALUES (@EventId, @IdFile);

	SELECT CAST(SCOPE_IDENTITY() AS INT);

END
