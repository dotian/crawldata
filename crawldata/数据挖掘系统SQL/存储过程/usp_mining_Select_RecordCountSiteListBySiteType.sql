if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_Select_RecordCountSiteListBySiteType]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_Select_RecordCountSiteListBySiteType
GO
create proc usp_mining_Select_RecordCountSiteListBySiteType
 @siteType int,
 @searchType int,
 @searchKey nvarchar(500)
as

    if(@searchType = 0 and @searchKey = '')
      select count(1) from SiteList where SiteType =@siteType 
      
    if(@searchType = 1)
      select count(1) from SiteList where SiteType =@siteType and PlateName = @searchKey 
      
    if(@searchType = 2)
      select count(1) from SiteList where SiteType =@siteType and SiteName = @searchKey 
go