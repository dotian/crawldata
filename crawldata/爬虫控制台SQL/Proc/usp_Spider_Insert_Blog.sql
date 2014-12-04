
--≤Â»Î ≤©øÕ
if exists (select * from dbo.sysobjects 
     where id = object_id(N'[usp_Spider_Insert_Blog]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_Spider_Insert_Blog
GO
create proc usp_Spider_Insert_Blog
@title nvarchar(200),
@content nvarchar(max),
@contentdate datetime,
@author nvarchar(100),
@srcurl varchar(500),
@siteid int,
@projectid int,
@createdate datetime
as
insert into Blog(title,content,contentdate,author,srcurl,siteid,projectid,createdate)
values(@title,@content,@contentdate,@author,@srcurl,@siteid,@projectid,@createdate)

go
