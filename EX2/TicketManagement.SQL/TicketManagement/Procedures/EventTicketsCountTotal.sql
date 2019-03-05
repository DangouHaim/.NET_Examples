CREATE PROCEDURE [dbo].[EventTicketsCountTotal]
	@EventId INT
AS
BEGIN
	DECLARE @R AS INT;

	SET @R = (SELECT COUNT(es.Id) FROM EventSeat AS es, EventArea AS ea
	WHERE es.EventAreaId = ea.Id AND ea.EventId = @EventId GROUP BY ea.Id);

	RETURN @R
END