USE [DataMingDB]
GO
/****** Object:  StoredProcedure [dbo].[usp_Spider_Select_TaskTarget]    Script Date: 11/29/2014 16:54:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[usp_Spider_Select_TaskTarget]  
 @projecttype int  
 as  
   
     --select projectid,keywords,siteid,siteurl,siteEncoding,templatecontent,  
     --  classname,crawlpagecount,runstatus,A.tid   
     --  from view_SelectTarget A ,TestProject B  
     --  where siteid in (select siteid from SiteList where sitetype is not null)  
     --   and projecttype = @projecttype   
     --   and runstatus = 0  
     --   and A.projectid = B.Pid  
     --   and A.tid = B.tid and B.TStatus = 0  
          
         select projectid,keywords,siteid,siteurl,siteEncoding,templatecontent,  
       classname,crawlpagecount,runstatus,tid,postContent,SiteUseType
       from view_SelectTarget   
       --select siteid from SiteList where sitetype is not null
       where siteid in (select siteid from SiteList where sitetype is not null) 
        and projecttype = 1   
        and runstatus = 0  
       
