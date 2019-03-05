CREATE PROCEDURE [dbo].[UpdateEventAreaPrice]
	@EventAreaId int,
	@Price decimal
AS
BEGIN
	DECLARE @C AS INT;
	SET @C = (SELECT COUNT(Id) FROM EventArea WHERE Id = @EventAreaId);
	if(@C = 0)
		BEGIN
			RETURN -1;
		END

	UPDATE EventArea SET Price = @Price WHERE Id = @EventAreaId;

	RETURN 1;
END
