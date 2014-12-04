USE [DataMingDB]
GO
/****** Object:  StoredProcedure [dbo].[usp_mining_Insert_ImportSiteDataBy_ProjectId]    Script Date: 11/12/2014 21:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[usp_mining_Insert_ImportSiteDataBy_ProjectId]

@projectid int,

@title varchar(200),

@srcurl varchar(600),

@sitename varchar(100),

@contentdate datetime,

@sitetype int,

@analysis int,

@showstatus int

as



declare @createdate varchar(50)

set @createdate = CONVERT(varchar(100), GETDATE(), 23)+' 12:00:00';



declare @exist int



select @exist = count(1 ) from SiteData 

 where ProjectId =@projectid and SiteType =@sitetype

  and SrcUrl = @srcurl



if(@exist<=0)

 begin	

    insert into SiteData(Title,ContentDate,SrcUrl,ProjectId,CreateDate,SiteType,SiteName,PlateName,Analysis,ShowStatus,Attention,Hot)

     values(@title,@contentdate,@srcurl,@projectid,@createdate,@sitetype,@sitename,@sitename,@analysis,@showstatus,0,0)

   select 1;

 end

else

   select 0;

