DELETE FROM dbo.Teams;
DELETE FROM dbo.Contestants;
DELETE FROM dbo.Venues;
DELETE FROM dbo.Tickets;
DELETE FROM dbo.Matches;
DELETE FROM dbo.Umpires;

Declare @Team1 UNIQUEIDENTIFIER = NEWID();
Declare @Contestant1 UNIQUEIDENTIFIER = NEWID();
Declare @Contestant2 UNIQUEIDENTIFIER = NEWID();

Declare @Team2 UNIQUEIDENTIFIER = NEWID();
Declare @Contestant3 UNIQUEIDENTIFIER = NEWID();
Declare @Contestant4 UNIQUEIDENTIFIER = NEWID();

Declare @Team3 UNIQUEIDENTIFIER = NEWID();
Declare @Contestant5 UNIQUEIDENTIFIER = NEWID();
Declare @Contestant6 UNIQUEIDENTIFIER = NEWID();

Declare @Team4 UNIQUEIDENTIFIER = NEWID();
Declare @Contestant7 UNIQUEIDENTIFIER = NEWID();
Declare @Contestant8 UNIQUEIDENTIFIER = NEWID();

Declare @Umpire UNIQUEIDENTIFIER = NEWID();

Declare @Match1 UNIQUEIDENTIFIER = NEWID();
Declare @Match2 UNIQUEIDENTIFIER = NEWID();
Declare @Match3 UNIQUEIDENTIFIER = NEWID();
Declare @Match4 UNIQUEIDENTIFIER = NEWID();
Declare @Match5 UNIQUEIDENTIFIER = NEWID();
Declare @Match6 UNIQUEIDENTIFIER = NEWID();
Declare @Match7 UNIQUEIDENTIFIER = NEWID();

Declare @Venue1 UNIQUEIDENTIFIER = NEWID();
Declare @Venue2 UNIQUEIDENTIFIER = NEWID();
Declare @Venue3 UNIQUEIDENTIFIER = NEWID();

Declare @Doctor1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Doctor2 UNIQUEIDENTIFIER = NEWID();

INSERT INTO dbo.Teams   (Id, Name, JoinDate) 
				VALUES  (@Team1, 'Team A', DATEADD(YY, -1, GETDATE())),
						(@Team2, 'Team B', DATEADD(MM, -11, GETDATE())),
						(@Team3, 'Team C', DATEADD(MM, -10, GETDATE())),
						(@Team4, 'Team D', DATEADD(YY, -1, GETDATE()))

INSERT INTO dbo.Contestants (Id, Name, TeamId, DominantHand, ImageUrl)
				VALUES (@Contestant1, 'Name 1', @Team1, 0, ''),
						(@Contestant2, 'Name 2', @Team1, 1, ''),
						(@Contestant3, 'Name 3', @Team2, 2, ''),
						(@Contestant4, 'Name 4', @Team2, 2, ''),
						(@Contestant5, 'Name 5', @Team3, 1, ''),
						(@Contestant6, 'Name 6', @Team3, 0, ''),
						(@Contestant7, 'Name 7', @Team4, 1, ''),
						(@Contestant8, 'Name 8', @Team4, 1, '')

INSERT INTO dbo.Umpires (Id, Name, TotalMatchStops)
			VALUES (@Umpire, 'Umpire 1', 3)

INSERT INTO dbo.Matches (Id, HomeTeamId, AwayTeamId, Date, HomeOdds, AwayOdds, UmpireId, WinningTeamId)
				 VALUES (@Match1, @Team1, @Team2, DATEADD(dd, 1, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire, null),
						(@Match2, @Team1, @Team3, DATEADD(dd, -5, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire, @Team1),
						(@Match3, @Team2, @Team3, DATEADD(dd, -8, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire, @Team3),
						(@Match4, @Team4, @Team1, DATEADD(dd, -4, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire, @Team1),
						(@Match5, @Team2, @Team4, DATEADD(dd, -10, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire, @Team2),
						(@Match6, @Team3, @Team4, DATEADD(dd, 3, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire, null),
						(@Match7, @Team1, @Team3, DATEADD(dd, -12, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire, @Team1)

INSERT INTO dbo.Venues (Id, Name, Capacity)
				VALUES  (@Venue1, 'Venue 1', 35),
						(@Venue2, 'Venue 2', 40),
						(@Venue3, 'Venue 3', 55)

INSERT INTO dbo.Tickets (Id, MatchId, Price, Seat, VenueId, DateSold)
			     VALUES (NEWID(), @Match1, 40, '3A', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 30, '3B', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 30, '3C', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 40, '1A', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 40, '2A', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 40, '2B', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 30, '4C', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 10, '2D', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 15, '1D', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 40, '4A', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 40, '2C', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 40, '3A', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 30, '3B', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 30, '3C', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 40, '1A', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 40, '2A', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 40, '2B', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 30, '4C', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 10, '2D', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 15, '1D', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 40, '4A', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match1, 40, '2C', @Venue1, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),

						(NEWID(), @Match3, 40, '3A', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 30, '3B', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 30, '3C', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 40, '1A', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 40, '3A', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 30, '3B', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 30, '3C', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 40, '1A', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 40, '3A', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 30, '3B', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 30, '3C', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 40, '1A', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 40, '3A', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 30, '3B', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 30, '3C', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match3, 40, '1A', @Venue2, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),

						(NEWID(), @Match6, 40, '3A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '3B', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '3C', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '1A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '2A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '2B', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '4C', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 10, '2D', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '3A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '3B', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '3C', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '1A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '2A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '2B', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '4C', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 10, '2D', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '3A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '3B', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '3C', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '1A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '2A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '2B', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '4C', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 10, '2D', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '3A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '3B', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '3C', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '1A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '2A', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 40, '2B', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 30, '4C', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5))),
						(NEWID(), @Match6, 10, '2D', @Venue3, DATEADD(dd, -10, GETDATE() + (RAND() * 5)))
