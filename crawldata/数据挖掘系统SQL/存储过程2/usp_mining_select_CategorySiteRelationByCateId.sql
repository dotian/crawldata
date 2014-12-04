use DataMiningDB
go


  if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_select_CategorySiteRelationByCateId]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_select_CategorySiteRelationByCateId
GO

 create proc usp_mining_select_CategorySiteRelationByCateId
  @cateid int
as

 select A.SiteId,A.SiteName,B.PlateName from dbo.CategorySiteRelation A, dbo.SiteList B
  where A.SiteId = B.SiteId and A.CateId = @cateid
 go 
 
 










































