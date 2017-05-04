USE [StatAnalytics]
GO

/****** Object:  StoredProcedure [dbo].[GetRankingsForKeyword]    Script Date: 5/1/2017 9:26:38 PM ******/
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

