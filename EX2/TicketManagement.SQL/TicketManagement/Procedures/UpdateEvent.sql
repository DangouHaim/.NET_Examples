CREATE PROCEDURE [dbo].[UpdateEvent]
	@EventId int,
	@Name varchar(120),
	@Description varchar(max),
	@LayoutId int,
	@EventDate datetime
AS
BEGIN
	DECLARE @COUNT AS INT;

	SET @COUNT = (SELECT COUNT(Id) FROM [Event] WHERE Id = @EventId);
	IF(@COUNT = 0)
		BEGIN
			RETURN -1;
		END

	SET @COUNT = (SELECT COUNT(Id) FROM Layout WHERE Id = @LayoutId);
	IF(@COUNT = 0)
		BEGIN
			RETURN -2;
		END

	SET @COUNT = (SELECT COUNT(Id) FROM [Event] WHERE [Name] = @Name);
	IF(@COUNT <> 0)
		BEGIN
			RETURN -3;
		END

	SET @COUNT = (SELECT COUNT(s.Id) FROM Seat AS s, Layout AS l, Area AS a
		WHERE l.id = @LayoutId AND a.LayoutId = l.Id AND s.AreaId = a.Id);
	IF(@COUNT = 0)
		BEGIN
			RETURN -4;
		END

	DECLARE @C1 AS INT;
	DECLARE @C2 AS INT;
	EXEC @C1 = EventTicketsCount @EventId
	EXEC @C2 = EventTicketsCountTotal @EventId
	IF(@C1 <> @C2)
		BEGIN
			RETURN -5;
		END

	BEGIN TRANSACTION
	BEGIN TRY
		DELETE FROM [EventArea] WHERE EventId = @EventId;

		UPDATE[Event] SET [Name] = @Name, [Description] = @Description, [LayoutId] = @LayoutId, [EventDate] = @EventDate WHERE [Id] = @EventId;

		INSERT INTO [EventArea] ([EventId], [LayoutId], [Description], [CoordX], [CoordY])
			SELECT e.[Id], e.[LayoutId], a.[Description], a.[CoordX], a.[CoordY] FROM [Event] as e 
			INNER JOIN [Area] as a ON a.[LayoutId] = e.[LayoutId]
			WHERE e.[Id] = @EventId;

		INSERT INTO [EventSeat] ([EventAreaId], [Row], [Number])
			SELECT ea.[Id], s.[Row], s.[Number] FROM [Event] as e
			INNER JOIN [Area] as a ON a.[LayoutId] = e.[LayoutId]
			INNER JOIN [EventArea] as ea ON ea.[EventId] = e.[Id]
			INNER JOIN [Seat] as s ON s.[AreaId] = a.[Id]
			WHERE e.[Id] = @EventId;

		COMMIT;
		RETURN 1
	END TRY

	BEGIN CATCH
		ROLLBACK;
		RETURN -6;
	END CATCH

	RETURN 1
END