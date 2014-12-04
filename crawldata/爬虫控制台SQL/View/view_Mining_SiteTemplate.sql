
if exists (select * from dbo.sysobjects where id = object_id(N'view_Mining_SiteTemplate') 
	and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view view_Mining_SiteTemplate
go
create view view_Mining_SiteTemplate
as
select SL.SiteId,SiteUrl,SiteType,SiteEncoding,T.Tid,T.TemplateName,T.TemplateContent,T.Remark from SiteList SL,Template T
 where SL.Tid =  T.tid and SL.UpdateDate is  null

go
