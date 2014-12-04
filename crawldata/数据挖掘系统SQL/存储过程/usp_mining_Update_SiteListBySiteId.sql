if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_Update_SiteListBySiteId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_Update_SiteListBySiteId
GO
create proc usp_mining_Update_SiteListBySiteId
@siteId int,
@sitename nvarchar(100),
@plateName nvarchar(100),
@siteUrl nvarchar(500),
@siteRank int,
@updateDate datetime
as

 update SiteList set SiteName = @sitename ,PlateName = @plateName,SiteUrl=@siteUrl,
   SiteRank = @siteRank ,UpdateDate = @updateDate,Remark = Remark+'[通过网页更新]'
 where SiteId = @siteId
go