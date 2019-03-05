CREATE PROCEDURE [dbo].[ToCart]
	@uid int,
	@eventSeatId int
AS
BEGIN
	INSERT INTO [Cart] (UserId, EventSeatId) VALUES (@uid, @eventSeatId);
	UPDATE EventSeat SET [State] = 1 WHERE Id = @eventSeatId;
	SELECT CAST(SCOPE_IDENTITY() AS INT);
END
