--≤Â»Î ¬€Ã≥
if exists (select * from dbo.sysobjects 
     where id = object_id(N'[usp_Spider_Insert_Forum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_Spider_Insert_Forum
GO
CREATE proc usp_Spider_Insert_Forum
@title nvarchar(200),
@content nvarchar(max),
@contentdate datetime,
@author nvarchar(100),
@srcurl varchar(500),
@pageview int,
@reply int,
@siteid int,
@projectid int,
@createdate datetime
as
insert into Forum(title,content,contentdate,author,srcurl,pageview,reply,siteid,projectid,createdate)
values(@title,@content,@contentdate,@author,@srcurl,@pageview,@reply,@siteid,@projectid,@createdate)
go