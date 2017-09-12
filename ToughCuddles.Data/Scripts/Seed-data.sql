DELETE FROM dbo.Tickets;
DELETE FROM dbo.Venues;
DELETE FROM dbo.Matches;
DELETE FROM dbo.Contestants;
DELETE FROM dbo.Teams;
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

Declare @Umpire1 UNIQUEIDENTIFIER = NEWID();
Declare @Umpire2 UNIQUEIDENTIFIER = NEWID();

Declare @Match1 UNIQUEIDENTIFIER = NEWID();
Declare @Match2 UNIQUEIDENTIFIER = NEWID();
Declare @Match3 UNIQUEIDENTIFIER = NEWID();
Declare @Match4 UNIQUEIDENTIFIER = NEWID();
Declare @Match5 UNIQUEIDENTIFIER = NEWID();
Declare @Match6 UNIQUEIDENTIFIER = NEWID();
Declare @Match7 UNIQUEIDENTIFIER = NEWID();
Declare @Match8 UNIQUEIDENTIFIER = NEWID();
Declare @Match9 UNIQUEIDENTIFIER = NEWID();
Declare @Match10 UNIQUEIDENTIFIER = NEWID();
Declare @Match11 UNIQUEIDENTIFIER = NEWID();
Declare @Match12 UNIQUEIDENTIFIER = NEWID();

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

INSERT INTO dbo.Contestants (Id, Name, TeamId, DominantHand, ImageUrl, HeightCm, WeightKg, ReachCm, StrikesMin)
				VALUES (@Contestant1, 'Gonzo the Great', @Team1, 0, 'https://lumiere-a.akamaihd.net/v1/images/character_themuppets_gonzo_9c3596c6.jpeg?region=0,0,300,300', 40, 4, 50, 3),
						(@Contestant2, 'Fozzie Bear', @Team1, 1, 'https://lumiere-a.akamaihd.net/v1/images/character_themuppets_fozzie_5314c3f1.jpeg?region=0,0,300,300', 31.2, 2.5, 39.3, 4),
						(@Contestant3, 'Animal', @Team2, 2, 'https://lumiere-a.akamaihd.net/v1/images/character_themuppets_animal_9d53d6e7.jpeg?region=0,0,300,300', 38.5, 3.2, 48, 3),
						(@Contestant4, 'Pepe the Prawn', @Team2, 2, 'https://lumiere-a.akamaihd.net/v1/images/character_themuppets_pepe_86d94b17.jpeg?region=0,0,300,300', 39.6, 3.6, 46.5, 5),
						(@Contestant5, 'Name 5', @Team3, 1, '', 42.3, 4.1, 52, 6),
						(@Contestant6, 'Name 6', @Team3, 0, '', 29.8, 2.2, 42.9, 2),
						(@Contestant7, 'Name 7', @Team4, 1, '', 33, 2.9, 36.9, 4),
						(@Contestant8, 'Name 8', @Team4, 1, '', 40.1, 5.5, 38.1, 3)

INSERT INTO dbo.Umpires (Id, Name, TotalMatchStops)
			VALUES  (@Umpire1, 'Umpire 1', 3),
					(@Umpire2, 'Umpire 2', 1)

INSERT INTO dbo.Matches (Id, HomeTeamId, AwayTeamId, Date, HomeOdds, AwayOdds, UmpireId, WinningTeamId)
				 VALUES (@Match1, @Team1, @Team2, DATEADD(dd, 1, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire2, null),
						(@Match2, @Team1, @Team3, DATEADD(dd, -5, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire1, @Team1),
						(@Match3, @Team2, @Team3, DATEADD(dd, -8, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire2, @Team3),
						(@Match4, @Team4, @Team1, DATEADD(dd, -4, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire1, @Team1),
						(@Match5, @Team2, @Team4, DATEADD(dd, -10, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire1, @Team2),
						(@Match6, @Team3, @Team4, DATEADD(dd, 3, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire2, null),
						(@Match7, @Team1, @Team3, DATEADD(dd, -12, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire1, @Team1),
						(@Match8, @Team3, @Team1, DATEADD(dd, 4, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire1, null),
						(@Match9, @Team4, @Team2, DATEADD(dd, 5, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire2, null),
						(@Match10, @Team2, @Team1, DATEADD(dd, 6, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire1, null),
						(@Match11, @Team2, @Team3, DATEADD(dd, 7, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire1, null),
						(@Match12, @Team1, @Team4, DATEADD(dd, 15, GETDATE()), (RAND() * 10) + RAND(), (RAND() * 10) + RAND(), @Umpire1, null)

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
