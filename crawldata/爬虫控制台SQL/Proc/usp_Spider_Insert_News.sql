--²åÈëĞÂÎÅ
if exists (select * from dbo.sysobjects 
     where id = object_id(N'[usp_Spider_Insert_News]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_Spider_Insert_News
GO
create proc usp_Spider_Insert_News
@title nvarchar(200),
@content nvarchar(max),
@contentdate datetime,
@srcurl varchar(500),
@siteid int,
@projectid int,
@createdate datetime
as
insert into News(title,content,contentdate,srcurl,siteid,projectid,createdate)
values(@title,@content,@contentdate,@srcurl,@siteid,@projectid,@createdate)

go