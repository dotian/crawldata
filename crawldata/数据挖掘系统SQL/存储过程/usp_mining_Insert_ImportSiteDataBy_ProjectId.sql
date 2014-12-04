
if exists (select * from dbo.sysobjects where id = object_id(N'[usp_mining_Insert_ImportSiteDataBy_ProjectId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure usp_mining_Insert_ImportSiteDataBy_ProjectId
GO
create proc usp_mining_Insert_ImportSiteDataBy_ProjectId
@projectid int,
@title varchar(200),
@srcurl varchar(600),
@sitename varchar(100),
@contentdate datetime,
@sitetype int
as

declare @createdate varchar(50)
set @createdate = CONVERT(varchar(100), GETDATE(), 23)+' 12:00:00';

declare @exist int

select @exist = count(1 ) from SiteData 
 where ProjectId =@projectid and SiteType =@sitetype
  and SrcUrl = @srcurl

if(@exist<=0)
 begin	
    insert into SiteData(Title,ContentDate,SrcUrl,ProjectId,CreateDate,SiteType,SiteName,Analysis,ShowStatus,Attention,Hot)
     values(@title,@contentdate,@srcurl,@projectid,@createdate,@sitetype,@sitename,0,0,0,0)
   select 1;
 end
else
   select 0;
go





