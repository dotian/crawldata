INSERT INTO [DataMingDB].[dbo].[SiteList]
           ([SiteName]
           ,[PlateName]
           ,[SiteUrl]
           ,[SiteType]
           ,[Tid]
           ,[SiteEncoding]
           ,[CreateDate]
           ,[SiteUseType])
     VALUES
           ('腾讯微博'
     		 ,'腾讯微博'
     		 ,'http://search.t.qq.com/index.php?k=<>&p={p}'
     		 , 5
     		 ,3047
     		 ,'utf-8'
     		 ,GETDATE()
     		 ,12)
     		   
  INSERT INTO [DataMingDB].[dbo].[ProjectDetail]
           ([ProjectId]
           ,[KeyWords]
           ,[ClassId]
           ,[SiteId]
           ,[StartDate]
           ,[RunStatus]
           ,[ProjectType])
     VALUES
           (13
           ,'韩泰'
           ,5
           ,100151
           ,GETDATE()
           ,0
           ,1)