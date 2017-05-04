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


