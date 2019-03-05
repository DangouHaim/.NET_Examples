CREATE PROCEDURE [dbo].[FromCart]
	@UID INT,
	@EventSeatId INT
AS
BEGIN
	UPDATE EventSeat SET [State] = 0 WHERE Id = @EventSeatId AND [State] = 1;
	DELETE FROM [Cart] WHERE UserId = @UID AND EventSeatId = @EventSeatId;
END