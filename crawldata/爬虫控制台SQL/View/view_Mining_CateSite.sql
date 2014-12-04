if exists (select * from dbo.sysobjects where id = object_id(N'view_Mining_CateSite') 
	and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view view_Mining_CateSite
go
create view view_Mining_CateSite
as
select A.CateId,CategoryName,ClassId,B.SiteId,B.SiteName,C.SiteUrl,C.SiteType,C.Tid,
  C.SiteEncoding,C.ContentEncoding
  from CategoryInfo A,
  CategorySiteRelation B,dbo.SiteList C 
 where A.CateId =B.CateId and B.SiteId = C.SiteId

go