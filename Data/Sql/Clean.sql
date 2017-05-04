
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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

