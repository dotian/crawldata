if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_Select_SiteListBySiteType]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_Select_SiteListBySiteType
GO
create proc usp_mining_Select_SiteListBySiteType
@sitetype int,
@pageSize int,
@pageIndex int,
@searchType int,
@searchKey nvarchar(500)
as
  
 declare @startindex int
  set @startindex = (@pageIndex-1)*@pageSize
  
  
  if (@searchType = 0 and @searchKey='')
    begin
        select top(@pageSize) RowNum,SiteId,SiteName,PlateName,SiteUrl,SiteRank,CreateDate,UpdateDate,Remark from 
		 (
		   -- 使用 row_number函数 新增一自增列用于 分页
		   select row_number() over(order by SiteId)as RowNum,SiteId,SiteName,PlateName,SiteUrl,SiteRank,CreateDate,UpdateDate,Remark
			 from SiteList 
			  where SiteType =@siteType
		  )as NewTB
		where NewTB.RowNum>@startindex
    end
  else if(@searchType =1)
    begin
	   select top(@pageSize) RowNum,SiteId,SiteName,PlateName,SiteUrl,SiteRank,CreateDate,UpdateDate,Remark from 
		 (
		   -- 使用 row_number函数 新增一自增列用于 分页
		   select row_number() over(order by SiteId)as RowNum,SiteId,SiteName,PlateName,SiteUrl,SiteRank,CreateDate,UpdateDate,Remark
			 from SiteList 
			  where SiteType =@siteType and PlateName = @searchKey 
		  )as NewTB
		where NewTB.RowNum>@startindex
	end
  else if(@searchType = 2)	
    begin
       select top(@pageSize) RowNum,SiteId,SiteName,PlateName,SiteUrl,SiteRank,CreateDate,UpdateDate,Remark from 
		 (
		   -- 使用 row_number函数 新增一自增列用于 分页
		   select row_number() over(order by SiteId)as RowNum,SiteId,SiteName,PlateName,SiteUrl,SiteRank,CreateDate,UpdateDate,Remark
			 from SiteList 
			  where SiteType =@siteType and SiteName = @searchKey 
		  )as NewTB
		where NewTB.RowNum>@startindex 
    end
go
