UPDATE [DataMingDB].[dbo].[SiteList]
   SET [SiteUseType] = 0

-- 0, changzhua; 1, is post; 2, is search; 3, is paging

UPDATE SiteList
SET [SiteUseType] = [SiteUseType]+4
where SiteUrl like '%<>%'

UPDATE SiteList
SET [SiteUseType] = [SiteUseType]+2
where SiteUrl like 'post:%'

UPDATE SiteList
SET [SiteUseType] = [SiteUseType]+8
where SiteUrl like '%{p}%'