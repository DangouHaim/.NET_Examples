CREATE PROCEDURE [dbo].[AddEvent]
	@Name varchar(120),
	@Description varchar(max),
	@LayoutId int,
	@EventDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @COUNT AS INT;
	DECLARE @E AS BIT;
	SET @E = 0;

	SET @COUNT = (SELECT COUNT(Id) FROM Layout WHERE Id = @LayoutId);
	IF(@COUNT = 0)
		BEGIN
			SET @E = 1;
			SELECT -1;
		END

	SET @COUNT = (SELECT COUNT(Id) FROM [Event] WHERE [Name] = @Name);
	IF(@COUNT <> 0)
		BEGIN
			SET @E = 1;
			SELECT -2;
		END

	SET @COUNT = (SELECT COUNT(s.Id) FROM Seat AS s, Layout AS l, Area AS a
		WHERE l.id = @LayoutId AND a.LayoutId = l.Id AND s.AreaId = a.Id);
	IF(@COUNT = 0)
		BEGIN
			SET @E = 1;
			SELECT -3;
		END

	IF(@E = 0)
		BEGIN
			BEGIN TRANSACTION
			BEGIN TRY
				INSERT INTO [Event] ([Name], [Description], [LayoutId], [EventDate]) VALUES (@Name, @Description, @LayoutId, @EventDate);

				DECLARE @ID AS INT;

				SET @ID = CAST(SCOPE_IDENTITY() AS INT);

				INSERT INTO [EventArea] ([EventId], [LayoutId], [Description], [CoordX], [CoordY], [Price])
					SELECT e.[Id], e.[LayoutId], a.[Description], a.[CoordX], a.[CoordY], 0 FROM [Event] as e 
					INNER JOIN [Area] as a ON a.[LayoutId] = e.[LayoutId]
					WHERE e.[Id] = (SELECT IDENT_CURRENT('[Event]'));

				INSERT INTO [EventSeat] ([EventAreaId], [Row], [Number])
					SELECT ea.[Id], s.[Row], s.[Number] FROM [Event] as e
					INNER JOIN [Area] as a ON a.[LayoutId] = e.[LayoutId]
					INNER JOIN [EventArea] as ea ON ea.[EventId] = e.[Id]
					INNER JOIN [Seat] as s ON s.[AreaId] = a.[Id]
					WHERE e.[Id] = (SELECT IDENT_CURRENT('[Event]'));

				COMMIT;
				RETURN @ID;
			END TRY

			BEGIN CATCH
				ROLLBACK;
				SELECT -3;
			END CATCH
		END
	ELSE
	BEGIN
		SELECT - 4
	END
END