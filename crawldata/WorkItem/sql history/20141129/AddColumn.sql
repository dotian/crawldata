ALTER TABLE dbo.SiteList ADD
	SiteUseType int NULL
	
---- Change view:	
--SELECT     PD.ProjectId, PD.ProjectType, PD.EndDate, PD.KeyWords, PD.SiteId, SL.SiteUrl, SL.SiteEncoding, T.TemplateContent, PD.RunStatus, PC.ClassName, 
--                      PC.CrawlPageCount, SL.Tid, SL.PostContent, SL.SiteUseType
--FROM         dbo.ProjectDetail AS PD INNER JOIN
--                      dbo.SiteList AS SL ON PD.SiteId = SL.SiteId INNER JOIN
--                      dbo.ProjectClass AS PC ON PD.ClassId = PC.ClassId LEFT OUTER JOIN
--                      dbo.Template AS T ON SL.Tid = T.Tid
