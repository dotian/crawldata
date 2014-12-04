
--插入 运行记录
if exists (select * from dbo.sysobjects 
     where id = object_id(N'[usp_Spider_Insert_RunRecord]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_Spider_Insert_RunRecord
GO
CREATE proc usp_Spider_Insert_RunRecord
@projecttypeId int,
@createdate datetime
as
declare @maxforumid int,
         @maxnewsid int,
         @maxblogid int,
         @maxmicroblogid int
        

select @maxforumid = max(cid) from Forum
select @maxnewsid = max(cid) from News
select @maxblogid = max(cid) from Blog
select @maxmicroblogid = max(cid) from MicroBlog

if(@maxforumid is null)
 set @maxforumid = 0;
 
if(@maxnewsid is null)
 set @maxnewsid = 0;
 
if(@maxblogid is null)
 set @maxblogid = 0;
 
if(@maxmicroblogid is null)
 set @maxmicroblogid = 0;
 
insert into RunRecord(ProjectTypeId,MaxForumId,MaxNewsId,MaxBlogId,MaxMicroBlogId,CreateDate)
values(@projecttypeId,@maxforumid,@maxnewsid,@maxblogid,@maxmicroblogid,@createdate)
go