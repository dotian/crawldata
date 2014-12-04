if exists (select * from dbo.sysobjects 
     where id = object_id(N'[usp_Spider_DeleteRepeat]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_Spider_DeleteRepeat
GO

create proc usp_Spider_DeleteRepeat
-- É¾³ýÖØ¸´Êý¾Ý
as

delete Forum where [cid] not in( 
select min([cid]) from Forum   
group by (Title + SrcUrl+SiteId+ProjectId)) 

delete Blog where [cid] not in( 
select min([cid]) from Blog   
group by(Title + SrcUrl+SiteId+ProjectId)) 


delete News where [cid] not in( 
select min([cid]) from News   
group by (Title + SrcUrl+SiteId+ProjectId)) 

delete MicroBlog where [cid] not in( 
select min([cid]) from MicroBlog   
group by (Title + SrcUrl+SiteId+ProjectId)) 
go
