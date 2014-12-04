if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_Insert_SiteListBySiteType]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_Insert_SiteListBySiteType
GO
create proc usp_mining_Insert_SiteListBySiteType
@sitename nvarchar(100),
@plateName nvarchar(100),
@siteUrl nvarchar(500),
@siteType int,
@siteRank int,
@createdate datetime
as
insert into SiteList(SiteName,PlateName,SiteUrl,SiteType,SiteRank,CreateDate)
 values(@sitename,@plateName,@siteUrl,@siteType,@siteRank,@createdate)

go