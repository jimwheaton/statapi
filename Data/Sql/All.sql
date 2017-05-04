SET ANSI_PADDING ON
GO

/****** Object:  Index [NonClusteredIndex-20170501-185227]    Script Date: 5/1/2017 8:36:47 PM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20170501-185227] ON [dbo].[Keywords]
(
	[Id] ASC
)
INCLUDE ( 	[Phrase]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [NonClusteredIndex-20170501-185335]    Script Date: 5/1/2017 8:37:50 PM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20170501-185335] ON [dbo].[Dates]
(
	[Id] ASC,
	[DateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRankingsForKeyword] 
	@Site NVARCHAR(MAX) = NULL,
	@Market NVARCHAR(MAX) = NULL,
	@Device NVARCHAR(MAX) = NULL,
	@Phrase NVARCHAR(MAX) = NULL,
	@Start NVARCHAR(MAX) = NULL,
	@End NVARCHAR(MAX) = NULL
AS
BEGIN
	
	SELECT s.Name as Site,
		   m.Name as Market,
		   d.Name as Device,
		   k.Phrase as Keyword,
		   dt.DateTime as Date,
		   kr.Bing,
		   kr.Google,
		   kr.GoogleBaseRank,
		   kr.Yahoo
	FROM KeywordRanks kr
	INNER JOIN Sites s ON kr.SiteId = s.Id
	INNER JOIN Markets m ON kr.MarketId = m.Id
	INNER JOIN Devices d ON kr.DeviceId = d.Id
	INNER JOIN Keywords k ON kr.KeywordId = k.Id
	INNER JOIN Dates dt ON kr.DateId = dt.Id
	WHERE (@Site IS NULL OR @Site = s.Name)
	AND (@Market IS NULL OR @Market = m.Name)
	AND (@Device IS NULL OR @Device = d.Name)
	AND (@Phrase IS NULL OR @Phrase = k.Phrase)
	AND (@Start IS NULL OR @Start <= dt.DateTime)
	AND (@End IS NULL OR @End >= dt.DateTime)
	ORDER BY k.Phrase, s.Name, m.Name, d.Name, dt.DateTime
END
GO


CREATE PROCEDURE [dbo].[GetWeightedRankingsForKeyword] 
	@Site NVARCHAR(MAX) = NULL,
	@Market NVARCHAR(MAX) = NULL,
	@Device NVARCHAR(MAX) = NULL,
	@Phrase NVARCHAR(MAX) = NULL,
	@Start NVARCHAR(MAX) = NULL,
	@End NVARCHAR(MAX) = NULL
AS
BEGIN

	DECLARE @MaxGlobalMonthlySearches FLOAT;
	SET @MaxGlobalMonthlySearches = (SELECT MAX(GlobalMonthlySearches) FROM Keywords)
			
	SELECT s.Name as Site,
		   m.Name as Market,
		   d.Name as Device,
		   k.Phrase as Keyword,
		   dt.DateTime as Date,
		   ROUND((kr.Bing * (k.GlobalMonthlySearches / @MaxGlobalMonthlySearches)), 2) AS Bing,
		   ROUND((kr.Google * (k.GlobalMonthlySearches / @MaxGlobalMonthlySearches)), 2) AS Google,
		   ROUND((kr.GoogleBaseRank * (k.GlobalMonthlySearches / @MaxGlobalMonthlySearches)), 2) AS GoogleBaseRank,
		   ROUND((kr.Yahoo * (k.GlobalMonthlySearches / @MaxGlobalMonthlySearches)), 2) AS Yahoo
	FROM KeywordRanks kr
	INNER JOIN Sites s ON kr.SiteId = s.Id
	INNER JOIN Markets m ON kr.MarketId = m.Id
	INNER JOIN Devices d ON kr.DeviceId = d.Id
	INNER JOIN Keywords k ON kr.KeywordId = k.Id
	INNER JOIN Dates dt ON kr.DateId = dt.Id
	WHERE (@Site IS NULL OR @Site = s.Name)
	AND (@Market IS NULL OR @Market = m.Name)
	AND (@Device IS NULL OR @Device = d.Name)
	AND (@Phrase IS NULL OR @Phrase = k.Phrase)
	AND (@Start IS NULL OR @Start <= dt.DateTime)
	AND (@End IS NULL OR @End >= dt.DateTime)
	ORDER BY k.Phrase, s.Name, m.Name, d.Name, dt.DateTime
END
GO

CREATE PROCEDURE LoadModelFromStaging 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO Sites 
	(Name)
	SELECT DISTINCT Site FROM DataStaging

	INSERT INTO Markets 
	(Name)
	SELECT DISTINCT Market FROM DataStaging

	INSERT INTO Keywords 
	(Phrase, GlobalMonthlySearches)
	SELECT DISTINCT Keyword, GlobalMonthlySearches FROM DataStaging

	INSERT INTO Devices 
	(Name)
	SELECT DISTINCT Device FROM DataStaging

	INSERT INTO Dates
	(DateTime)
	SELECT DISTINCT [Date] FROM DataStaging 

	INSERT INTO KeywordRanks 
	(SiteId
	,MarketId
	,DeviceId
	,KeywordId
	,DateId
	,Bing
	,Google
	,GoogleBaseRank
	,Yahoo
	,BingUrl
	,GoogleUrl
	,YahooUrl
	)
	SELECT s.Id
	, m.Id
	, d.Id
	, k.Id
	, dt.Id
	, ds.Bing
	, ds.Google
	, ds.GoogleBaseRank
	, ds.Yahoo
	, ds.BingUrl
	, ds.GoogleUrl
	, ds.YahooUrl
	FROM DataStaging ds
	INNER JOIN Sites s ON s.Name = ds.Site
	INNER JOIN Markets m ON m.Name = ds.Market
	INNER JOIN Devices d ON d.Name = ds.Device
	INNER JOIN Keywords k ON k.Phrase = ds.Keyword
	INNER JOIN Dates dt ON dt.DateTime = ds.Date

END
GO


CREATE PROCEDURE [dbo].[Clean]
AS
BEGIN

--Date FK Contraint
ALTER TABLE [dbo].[KeywordRanks] DROP CONSTRAINT [FK_KeywordRanks_Dates_DateId]

--Device FK Contraint
ALTER TABLE [dbo].[KeywordRanks] DROP CONSTRAINT [FK_KeywordRanks_Devices_DeviceId]

--Keyword FK contraint
ALTER TABLE [dbo].[KeywordRanks] DROP CONSTRAINT [FK_KeywordRanks_Keywords_KeywordId]

-- Markets FK contraint
ALTER TABLE [dbo].[KeywordRanks] DROP CONSTRAINT [FK_KeywordRanks_Markets_MarketId]

--Sites FK constraint
ALTER TABLE [dbo].[KeywordRanks] DROP CONSTRAINT [FK_KeywordRanks_Sites_SiteId]

TRUNCATE TABLE datastaging
TRUNCATE TABLE keywordranks
TRUNCATE TABLE dates
TRUNCATE TABLE devices
TRUNCATE TABLE keywords
TRUNCATE TABLE markets
TRUNCATE TABLE sites

--Date FK Contraint
ALTER TABLE [dbo].[KeywordRanks]  WITH CHECK ADD  CONSTRAINT [FK_KeywordRanks_Dates_DateId] FOREIGN KEY([DateId])
REFERENCES [dbo].[Dates] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[KeywordRanks] CHECK CONSTRAINT [FK_KeywordRanks_Dates_DateId]

--Device FK Contraint
ALTER TABLE [dbo].[KeywordRanks]  WITH CHECK ADD  CONSTRAINT [FK_KeywordRanks_Devices_DeviceId] FOREIGN KEY([DeviceId])
REFERENCES [dbo].[Devices] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[KeywordRanks] CHECK CONSTRAINT [FK_KeywordRanks_Devices_DeviceId]

--Keyword FK contraint
ALTER TABLE [dbo].[KeywordRanks]  WITH CHECK ADD  CONSTRAINT [FK_KeywordRanks_Keywords_KeywordId] FOREIGN KEY([KeywordId])
REFERENCES [dbo].[Keywords] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[KeywordRanks] CHECK CONSTRAINT [FK_KeywordRanks_Keywords_KeywordId]

-- Markets FK contraint
ALTER TABLE [dbo].[KeywordRanks]  WITH CHECK ADD  CONSTRAINT [FK_KeywordRanks_Markets_MarketId] FOREIGN KEY([MarketId])
REFERENCES [dbo].[Markets] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[KeywordRanks] CHECK CONSTRAINT [FK_KeywordRanks_Markets_MarketId]

--Sites FK constraint
ALTER TABLE [dbo].[KeywordRanks]  WITH CHECK ADD  CONSTRAINT [FK_KeywordRanks_Sites_SiteId] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Sites] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[KeywordRanks] CHECK CONSTRAINT [FK_KeywordRanks_Sites_SiteId]

END

GO


