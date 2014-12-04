INSERT [SiteList] ([SiteName],[PlateName],[SiteUrl],[SiteType],[Tid],[SiteEncoding],[CreateDate],[UpdateDate],[Remark]) VALUES ( N'阿尔法罗密欧论坛',N'汽车之家',N'http://club.autohome.com.cn/bbs/forum-c-2288-1.html',1,1253,N'GB2312',N'2014/8/16 17:29:02',null,N'20140817')
INSERT [SiteList] ([SiteName],[PlateName],[SiteUrl],[SiteType],[Tid],[SiteEncoding],[CreateDate],[UpdateDate],[Remark]) VALUES ( N'上海论坛',N'汽车之家',N'http://club.autohome.com.cn/bbs/forum-a-100024-1.html',1,1253,N'GB2312',N'2014/8/16 17:29:02',null,N'20140817')
INSERT [SiteList] ([SiteName],[PlateName],[SiteUrl],[SiteType],[Tid],[SiteEncoding],[CreateDate],[UpdateDate],[Remark]) VALUES ( N'自驾游论坛',N'汽车之家',N'http://club.autohome.com.cn/bbs/forum-o-200042-1.html',1,1253,N'GB2312',N'2014/8/16 17:29:02',null,N'20140817')
INSERT [SiteList] ([SiteName],[PlateName],[SiteUrl],[SiteType],[Tid],[SiteEncoding],[CreateDate],[UpdateDate],[Remark]) VALUES ( N'摩托车论坛',N'汽车之家',N'http://club.autohome.com.cn/bbs/forum-o-200063-1.html',1,1253,N'GB2312',N'2014/8/16 17:29:02',null,N'20140817')

INSERT INTO [DataMingDB].[dbo].[CategorySiteRelation]
           ([SiteId],[SiteName],[CateId],[CreateData])
     VALUES
           (100088,'阿尔法罗密欧论坛',12,GETDATE())
INSERT INTO [DataMingDB].[dbo].[CategorySiteRelation]
           ([SiteId],[SiteName],[CateId],[CreateData])
     VALUES
           (100089,'上海论坛',12,GETDATE())
INSERT INTO [DataMingDB].[dbo].[CategorySiteRelation]
           ([SiteId],[SiteName],[CateId],[CreateData])
     VALUES
           (100090,'自驾游论坛',12,GETDATE())
INSERT INTO [DataMingDB].[dbo].[CategorySiteRelation]
           ([SiteId],[SiteName],[CateId],[CreateData])
     VALUES
           (100091,'摩托车论坛',12,GETDATE()) 

INSERT INTO [DataMingDB].[dbo].[Template]
           ([Tid]
           ,[TemplateName]
           ,[TemplateContent]
           ,[Remark]
           ,[UpdateDate])
     VALUES
           (1253
           ,'汽车之家140818'
           ,''
           ,'140818'
           ,'20140818')
GO

-- update url for paging (2014/8//23)
UPDATE [DataMingDB].[dbo].[SiteList]
   SET [SiteUrl] = 'http://club.autohome.com.cn/bbs/forum-c-2288-{p}.html'
 WHERE SiteId = 100088
 
UPDATE [DataMingDB].[dbo].[SiteList]
   SET [SiteUrl] = 'http://club.autohome.com.cn/bbs/forum-a-100024-{p}.html'
 WHERE SiteId = 100089
 
UPDATE [DataMingDB].[dbo].[SiteList]
   SET [SiteUrl] = 'http://club.autohome.com.cn/bbs/forum-o-200042-{p}.html'
 WHERE SiteId = 100090

UPDATE [DataMingDB].[dbo].[SiteList]
   SET [SiteUrl] = 'http://club.autohome.com.cn/bbs/forum-o-200063-{p}.html'
 WHERE SiteId = 100091
 
UPDATE [DataMingDB].[dbo].[SiteList]
   SET [Tid] = 1253
 WHERE Tid in (22, 373, 375, 1251)
 