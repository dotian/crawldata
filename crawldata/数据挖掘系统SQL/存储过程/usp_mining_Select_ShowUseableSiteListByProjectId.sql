if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_Select_ShowUseableSiteListByProjectId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_Select_ShowUseableSiteListByProjectId
GO
create proc usp_mining_Select_ShowUseableSiteListByProjectId
   @projectId int,
   @siteType int,
   @pageSize int,
   @pageIndex int
 as
  declare @startindex int
  set @startindex = (@pageIndex-1)*@pageSize
  
   select top(@pageSize) RowNum,SiteId,SiteName,PlateName,SiteUrl from 
     (
	   -- 使用 row_number函数 新增一自增列用于 分页
	   select row_number() over(order by SiteId)as RowNum,SiteId,SiteName,PlateName,SiteUrl
		 from SiteList 
		  where SiteType =@siteType and SiteId not in
		  (select SiteId from ProjectDetail where ProjectId = @projectId)
	  )as NewTB
	where NewTB.RowNum>@startindex
go
