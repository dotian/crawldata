if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_Select_RecordCountUseableSiteListByProjectId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_Select_RecordCountUseableSiteListByProjectId
GO
create proc usp_mining_Select_RecordCountUseableSiteListByProjectId
 @projectId int,
 @siteType int
as
   select count(1) from SiteList
	  where SiteType =@siteType and SiteId not in
	  (select SiteId from ProjectDetail where ProjectId = @projectId)
go
