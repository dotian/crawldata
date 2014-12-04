-----视图
if exists (select * from dbo.sysobjects where id = object_id(N'view_SelectTarget') 
	and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view view_SelectTarget
go
CREATE view [dbo].[view_SelectTarget]
as
select PD.ProjectId,Pd.ProjectType,EndDate,Pd.KeyWords,Pd.SiteId,SL.SiteUrl,
    SL.SiteEncoding,T.TemplateContent,Pd.RunStatus,PC.ClassName,Pc.CrawlPageCount,T.Tid
 from ProjectDetail PD,ProjectClass PC ,SiteList SL,Template T
where PD.SiteId = SL.SiteId and PD.ClassId = PC.ClassId and SL.Tid = T.Tid 

go