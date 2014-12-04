if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_insert_hankook]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_insert_hankook
GO

create proc usp_mining_insert_hankook
@title varchar(255),
@content text,
@url varchar(1255),
@analysis int,
@tags varchar(255),
@published datetime,
@contend varchar(255),
@type varchar(255),
@sitename varchar(50)
as
  insert into dbo.hankook(title,[content],url,analysis,tags,published,contend,[type],sitename)
 values(@title,@content,@url,@analysis,@tags,@published,@contend,@type,@sitename)
go






















