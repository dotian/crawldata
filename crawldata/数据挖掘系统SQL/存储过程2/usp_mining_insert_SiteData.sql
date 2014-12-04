use DataMiningDB
go

  if exists (select * from dbo.sysobjects where id = object_id( N'[usp_mining_insert_SiteData]')
     and  OBJECTPROPERTY(id, N'IsProcedure') = 1) 
 drop procedure usp_mining_insert_SiteData
GO

 create proc usp_mining_insert_SiteData
  @cid int,
  @title varchar(500),
  @content varchar(max),
  @contentdate datetime,
  @srcurl varchar(500),
  @projectid int,
  @createdate datetime,
  @sitetype int,
  @sitename varchar(50)
as
  insert into SiteData (Cid,Title,Content,ContentDate,SrcUrl,ProjectId,CreateDate,SiteType,SiteName)
	values(@cid,@title,@content,@contentdate,@srcurl,@projectid,@createdate,@sitetype,@sitename)
 go 
 
 



















