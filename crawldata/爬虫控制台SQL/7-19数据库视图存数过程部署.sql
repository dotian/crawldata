







--插入 新浪微博
if exists (select * from dbo.sysobjects 
     where id = object_id(N'[usp_Spider_Insert_MicroBlog]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_Spider_Insert_MicroBlog
GO
create proc usp_Spider_Insert_MicroBlog
@title nvarchar(200),
@content nvarchar(max),
@contentdate datetime,
@author nvarchar(100),
@srcurl varchar(500),
@siteid int,
@projectid int,
@createdate datetime
as
insert into MicroBlog(title,content,contentdate,author,srcurl,siteid,projectid,createdate)
values(@title,@content,@contentdate,@author,@srcurl,@siteid,@projectid,@createdate)

go

-----------------------------------------------------------------------------------------------------
---------------------------------以下是功能性的 存储过程--------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------



--exec usp_Spider_DeleteRepeat









