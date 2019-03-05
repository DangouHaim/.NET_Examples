CREATE PROCEDURE [dbo].[Buy]
	@UID INT,
	@EventSeatId INT
AS
BEGIN
	BEGIN TRANSACTION
	BEGIN TRY

		DECLARE @B AS DECIMAL;
		SET @B = (SELECT [Balance] FROM [User] WHERE Id = @UID);

		DECLARE @P AS DECIMAL;
		SET @P = (SELECT a.Price FROM EventSeat AS s, EventArea AS a
			WHERE s.Id = @EventSeatId AND s.EventAreaId = a.Id);

		IF(@P > @B)
			BEGIN
				SELECT -1;
				ROLLBACK;
			END
		ELSE
			BEGIN
				DECLARE @EventId AS INT;
				DECLARE @AreaId AS INT;

				DECLARE @EventName AS NVARCHAR(255);
				DECLARE @EventDescription AS NVARCHAR(255);
				DECLARE @EventDate AS DATETIME;
				DECLARE @AreaDescription AS NVARCHAR(255);
				DECLARE @Row AS INT;
				DECLARE @Number AS INT;

				SET @EventId = (SELECT e.Id FROM EventSeat AS s, EventArea AS a, [Event] AS e
					WHERE s.Id = @EventSeatId AND s.EventAreaId = a.Id AND e.Id = a.EventId);

				SET @AreaId = (SELECT a.Id FROM EventSeat AS s, EventArea AS a
					WHERE s.Id = @EventSeatId AND s.EventAreaId = a.Id);

				SET @EventName = (SELECT Name FROM [Event] WHERE Id = @EventId);
				SET @EventDescription = (SELECT [Description] FROM [Event] WHERE Id = @EventId);
				SET @EventDate = (SELECT EventDate FROM [Event] WHERE Id = @EventId);
				SET @AreaDescription = (SELECT [Description] FROM [EventArea] WHERE Id = @AreaId);
				SET @Row = (SELECT [Row] FROM EventSeat WHERE Id = @EventSeatId);
				SET @Number = (SELECT Number FROM EventSeat WHERE Id = @EventSeatId);
		
				DECLARE @Hit AS INT;
				SET @Hit = (SELECT COUNT(Id) FROM Purchase 
					WHERE UserId = @UID AND EventName = @EventName AND EventDescription = @EventDescription
					AND EventDate = @EventDate AND AreaDescription = @AreaDescription AND SeatRow = @Row
					AND SeatNumber = @Number);

				IF(@Hit > 0)
					BEGIN
						SELECT -2;
						ROLLBACK;
					END
				ELSE
					BEGIN
						INSERT INTO Purchase (UserId, EventName, EventDescription, EventDate, AreaDescription, SeatRow, SeatNumber, [Date])
							VALUES (@UID, @EventName, @EventDescription, @EventDate, @AreaDescription, @Row, @Number, GETDATE());

						SELECT CAST(SCOPE_IDENTITY() AS INT);

						SET @B = @B - @P;

						UPDATE [User] SET Balance = @B WHERE Id = @UID;
						UPDATE EventSeat SET [State] = 2 WHERE Id = @EventSeatId;
						DELETE FROM [Cart] WHERE UserId = @UID AND EventSeatId = @EventSeatId;

						COMMIT;
					END
			END
	END TRY

	BEGIN CATCH
		ROLLBACK;
		SELECT -3;
	END CATCH
	

END