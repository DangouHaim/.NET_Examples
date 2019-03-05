CREATE PROCEDURE [dbo].[DeleteEvent]
	@EventId int
AS
BEGIN
	DECLARE @C AS INT;
	SET @C = (SELECT COUNT(es.Id) FROM EventSeat AS es, EventArea AS ea
	WHERE ([State] <> 0) AND EventAreaId = ea.Id AND ea.EventId = @EventId);

	IF(@C > 0)
		BEGIN
			RETURN -1;
		END

	DELETE FROM Event WHERE Id = @EventId;

	RETURN 1;
END
