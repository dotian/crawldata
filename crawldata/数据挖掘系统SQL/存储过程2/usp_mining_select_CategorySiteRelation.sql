use DataMiningDB
go


  if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_select_CategorySiteRelationByCateId]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_select_CategorySiteRelationByCateId
GO

 create proc usp_mining_select_CategorySiteRelationByCateId
  @cateid int
as

 select A.SiteId,A.SiteName,B.PlateName,B.SiteUrl from dbo.CategorySiteRelation A, dbo.SiteList B
  where A.SiteId = B.SiteId and A.CateId = @cateid
 go 
 
 
 exec usp_mining_select_CategorySiteRelationByCateId 1



--select * from dbo.CategorySiteRelation


select CateId,CategoryName,ClassId from dbo.CategoryInfo




select * from Template where TemplateName like '%博客%'


/*

765	搜狗博客
85	zol博客
766	新浪博客
922	赛迪网IT博客
973	沪江博客
991	腾讯博客
*/

select * from dbo.SiteList where tid in
(765,85,766,922,973,991)


insert into CategorySiteRelation(SiteId,SiteName,CateId,CreateData)

select '88036','新浪博客','5','2013-11-4' union all 
 select '88038','搜狗博客','5','2013-11-4' union all 
 select '75026','写手热评','5','2013-11-4' union all 
 select '63562','沪江博客最新文章','5','2013-11-4' union all 
 select '70148','程鸿-CIOAge.com','5','2013-11-4' 





select * from CategorySiteRelation






select CateId,CategoryName,count(1) as C, from dbo.CategoryInfo A, 






<table style=' width:100%;' border='0' cellpadding='0' cellspacing='0'><tr><th style='width:15%;'></th><th style='width:30%;'>分类名称</th><th style='width:15%;'>站点总数</th><th style='width:30%;'>创建时间</th></tr><tr><td><input type='checkbox' id='1' value='1'/></td><td>奇虎论坛搜索</td><td>1</td><td>1</td></tr></table>













