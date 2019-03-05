CREATE PROCEDURE [dbo].[GetAttachedFilesForEvent]
	@EventId int
AS
BEGIN

	SELECT f.Id, f.Url FROM EventFile AS ef, [File] AS f WHERE EventId = @EventId AND f.Id = ef.FileId;

END
