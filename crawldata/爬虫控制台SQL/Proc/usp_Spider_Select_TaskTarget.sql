
--≤È—Ø »ŒŒÒ
if exists (select * from dbo.sysobjects 
     where id = object_id(N'[usp_Spider_Select_TaskTarget]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_Spider_Select_TaskTarget
GO
create proc usp_Spider_Select_TaskTarget
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
      classname,crawlpagecount,runstatus,tid 
      from view_SelectTarget 
      where siteid in (select siteid from SiteList where sitetype is not null)
       and projecttype = 1 
       and runstatus = 0
    
     
go

--exec usp_Spider_Select_TaskTarget 1











