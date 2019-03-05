CREATE PROCEDURE [dbo].[EventTicketsCount]
	@EventId INT
AS
BEGIN
	DECLARE @R AS INT;

	SET @R = (SELECT COUNT(es.Id) FROM EventSeat AS es, EventArea AS ea
	WHERE es.EventAreaId = ea.Id AND ea.EventId = @EventId AND es.State = 0 GROUP BY ea.Id);

	RETURN @R
END
